
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using theflamesofwar_bot.Models;
using theflamesofwar_bot.Repositories;
using System.Collections.Generic;
using System;

namespace theflamesofwar_bot.Core;
public static class BotButtonsProcessing
{
    public static ITelegramBotClient Bot { get; set; }
    public static Models.User CurrentPlayer { get; set; }
    public static int CurrentPosition { get; set; } = 1;
    private static int button11Value = 0;
    private static int button12Value = 0;
    private static int button21Value = 0;
    private static int button22Value = CurrentPosition;
    private static int button23Value = 0;
    private static int button31Value = 0;
    private static int button32Value = 0;

    public static async Task UpdateStatusAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string codeOfButton, Models.User user, Table table)
    {
        //if (true)
        //{
        //    BattleButtonAsync(update.CallbackQuery.Message);
        //}
        //else
        if (codeOfButton.StartsWith("MapId="))
        {
            Guid mapGuid;
            Guid.TryParse(codeOfButton.Substring(6), out mapGuid);
            var s = new SqlMapRepository();

            MapGenerator.map = s.Get(mapGuid);
            MapGenerator.RestoreMap(MapGenerator.map);
            MapButtonAsync(update.CallbackQuery.Message, table);

        }
        else if (codeOfButton == "22")
        {
            ObserveButtonAsync(update.CallbackQuery.Message);
        }
        else if (codeOfButton == "move")
        {
            MapButtonAsync(update.CallbackQuery.Message, table);
            return;
        }
        else if (codeOfButton == "CreateNew")
        {
            table.Open = false;
            var tr = new SqlTableRepository();
            tr.Update(table);
            tr.Save();
            await Bot.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Make you choice", replyMarkup: BotButtonsProcessing.CreateNewMap());
            return;
        }
        else if (codeOfButton == "ChooseMap")
        {
            var inlineKeyboard = ChooseMap();
            Bot.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Оберіть карту:", replyMarkup: inlineKeyboard);
        }
        else if (codeOfButton == "CreateRandomMap")
        {
            MapGenerator.CreateRandomMap();
            Lobby.CreateTable(user);
            CurrentPosition = 1;
            MapButtonAsync(update.CallbackQuery.Message, table);
        }
        else if (codeOfButton == "Reconnect")
        {
            var s = new SqlMapRepository();

            MapGenerator.map =  s.Get(table.MapId);
            MapGenerator.RestoreMap(MapGenerator.map);
            MapButtonAsync(update.CallbackQuery.Message, table);
        }
        else if (codeOfButton == "11" || codeOfButton == "12" || codeOfButton == "21" || codeOfButton == "23" || codeOfButton == "31" || codeOfButton == "32")
        {
            int result = 0;
            if (CommandValue(codeOfButton, out result))
            {
                if (result == 0)
                    return;
                var land = GetLand(result, true, table);
                string telegramMessage = $"У ячійці {result} {land.Name}";
                await botClient.SendTextMessageAsync(chatId: update.CallbackQuery.Message.Chat.Id, telegramMessage, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                if (land.Passability == Passabilities.Impossible)
                {
                    telegramMessage = $"Ви не можете сюди пройти";
                    await botClient.SendTextMessageAsync(chatId: update.CallbackQuery.Message.Chat.Id, telegramMessage, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                }
                else
                {
                    var thing = GetThing(result, false, table);
                    if (thing.ThingType != "Nothing")
                    {
                        telegramMessage = $"Ви знайшли {thing.Name}";
                        await botClient.SendTextMessageAsync(chatId: update.CallbackQuery.Message.Chat.Id, telegramMessage, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                    }

                    CurrentPosition = result;
                    Lobby.UpdateLobby(user, result);
                    MapButtonAsync(update.CallbackQuery.Message, table);
                }
            }
            else
            {
                string telegramMessage = "Ви не можете сюди рухатись";
                await botClient.SendTextMessageAsync(chatId: update.CallbackQuery.Message.Chat.Id, telegramMessage, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
            }
        }
        else if (codeOfButton == "observe")
        {
            ObserveAround(table);
            MapButtonAsync(update.CallbackQuery.Message, table);
        }
    }
    public static async Task MapButtonAsync(Message msg, Table table)
    {
        var evenLine = (CurrentPosition - 1) / 15 % 2 == 1;
        var firstInLine = CurrentPosition % 15 == 1;
        var lastInLine = CurrentPosition % 15 == 0;
        var firstLine = CurrentPosition <= 15;
        var lastLine = CurrentPosition > 135;


        button11Value = 0;
        button12Value = 0;
        button21Value = 0;
        button23Value = 0;
        button31Value = 0;
        button32Value = 0;

        if (!firstLine)
        {
            if (evenLine)
            {
                if (!firstInLine)
                    button11Value = CurrentPosition - 15 - 1;

                button12Value = CurrentPosition - 15;
            }
            else
            {
                button11Value = CurrentPosition - 15;

                if(!lastInLine)
                    button12Value = CurrentPosition - 15 + 1;
            }
        }
        if (!firstInLine)
            button21Value = CurrentPosition - 1;

        button22Value = CurrentPosition;

        if (!lastInLine)
            button23Value = CurrentPosition + 1;
        if (!lastLine)
        {
            if (evenLine)
            {
                if (!firstInLine)
                    button31Value = CurrentPosition + 15 - 1;

                button32Value = CurrentPosition + 15;
            }
            else
            {
                button31Value = CurrentPosition + 15;

                if (!lastInLine)
                    button32Value = CurrentPosition + 15 + 1;
            }
        }



        string button11 = " ";
        string button12 = " ";
        string button21 = " ";
        string button22 = "You here";
        string button23 = " ";
        string button31 = " ";
        string button32 = " ";

        setButtonText(ref button11, button11Value, table);
        setButtonText(ref button12, button12Value, table);
        setButtonText(ref button21, button21Value, table);
        setButtonText(ref button23, button23Value, table);
        setButtonText(ref button31, button31Value, table);
        setButtonText(ref button32, button32Value, table);

        var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        // first row
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData(button11, "11"),
                            InlineKeyboardButton.WithCallbackData(button12, "12"),

                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData(button21, "21"),
                            InlineKeyboardButton.WithCallbackData(button22, "22"),
                            InlineKeyboardButton.WithCallbackData(button23, "23"),
                        }
                        ,
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData(button31, "31"),
                            InlineKeyboardButton.WithCallbackData(button32, "32"),
                        }
                    });
        await Bot.SendTextMessageAsync(msg.Chat.Id, "Оберіть шлях:", replyMarkup: inlineKeyboard);
    }
    private static bool CommandValue(string codeOfButton, out int result)
    {
        switch (codeOfButton)
        {
            case "11":
                {
                    result = button11Value;
                    return true;
                }
            case "12":
                {
                    result = button12Value;
                    return true;
                }
            case "21":
                {
                    result = button21Value;
                    return true;
                }
            case "23":
                {
                    result = button23Value;
                    return true;
                }
            case "31":
                {
                    result = button31Value;
                    return true;
                }
            case "32":
                {
                    result = button32Value;
                    return true;
                }
            default:
                {
                    result = 0;
                    break;
                }
        }
        return false;
    }
    private static Land GetLand(int place, bool isOpen, Table table)
    {
        var cell = GetCell(place, isOpen, table);
        if (cell == null)
            return new Land();
        else if (cell.Land != null)
            return cell.Land;
        else if (cell.LandId != Guid.Empty)
        {
            var sqlLandRep = new SqlLandRepository();
            cell.Land = sqlLandRep.Get(cell.LandId);
            return cell.Land;
        }
        else
            return new Land();
        return null;
    }
    private static Thing GetThing(int place, bool isOpen, Table table)
    {
        var cell = GetCell(place, isOpen, table);
        if (cell == null)
            return new Thing();
        else if(cell.Thing != null)
            return cell.Thing;
        else if (cell.ThingId != Guid.Empty)
        {
            var sqlThingRep = new SqlThingRepository();
            cell.Thing = sqlThingRep.Get(cell.ThingId);
            return cell.Thing;
        }
        else
            return new Thing();
    }
    private static bool IsOpen(int place, Table table)
    {
        if (place == null)
            return false;
        var cell = GetCell(place, false, table);
        if (cell == null)
            return false;
        return cell.IsOpen;
    }
    private static TableCell GetCell(int place, bool isOpen, Table table)
    {
        var tableCellRepository = new SqlTableCellRepository();
        if (place == 0) return new TableCell();
        var y = (place - 1) / 15;
        var x = (place - 1) % 15;
        //var cell = MapGenerator.map.Cells[x, y];
        var cell = tableCellRepository.Get(table, x, y);
        if (cell != null && isOpen)
        {
            cell.IsOpen = true;
            tableCellRepository.Update(cell);
            tableCellRepository.Save();
        }
        return cell;
    }
    private static void setButtonText(ref string button, int buttonValue, Table table)
    {
        if (buttonValue != 0)
        {
            var pre = "";
            var post = "";
            if (IsOpen(buttonValue, table))
            {
                pre = GetLand(buttonValue, false,table).Emoji;
                post = GetThing(buttonValue,false ,table).Emoji;
            }
            button = $"{pre} {buttonValue.ToString()} {post}";
        }
    }
    public static async Task ObserveButtonAsync(Message msg)
    {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                        // first row
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Розвідка 🕵️‍♀️", "observe"),
                            InlineKeyboardButton.WithCallbackData("Рухатись 👣", "move"),

                        }
                    });
        await Bot.SendTextMessageAsync(msg.Chat.Id, "Оберіть дію:", replyMarkup: inlineKeyboard);

    }
    public static void ObserveAround(Table table)
    {
        GetLand(button11Value, true, table);
        GetLand(button12Value, true, table);
        GetLand(button21Value, true, table);
        GetLand(button23Value, true, table);
        GetLand(button31Value, true, table);
        GetLand(button32Value, true, table);
    }
    public static InlineKeyboardMarkup CreateNewMap()
    {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("CreateRandomMap", "CreateRandomMap"),
                            InlineKeyboardButton.WithCallbackData("Choose map", "ChooseMap"),
                        }
                    });
        return inlineKeyboard;
    }

    public static InlineKeyboardMarkup LoadMap()
    {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Back to current game", "Reconnect"),
                            InlineKeyboardButton.WithCallbackData("Create new", "CreateNew"),
                        }
                    });
        return inlineKeyboard;
    }
    public static InlineKeyboardMarkup ChooseMap()
    {
        var mapSql = new SqlMapRepository();
        var mapList = mapSql.GetList();

        var inline = CategoryKeyboard(mapList);//создаём клавиатуру
        return new InlineKeyboardMarkup(inline);
    }

    private static InlineKeyboardButton[][] CategoryKeyboard(IEnumerable<Map> mapList)
    {
        var rows = new List<InlineKeyboardButton[]>();

        foreach (var map in mapList)
        {
            if (map.Name != null)
            rows.Add(
                new[]
                {
                InlineKeyboardButton.WithCallbackData(map.Name, $"MapId={map.Id}"),
                }
    
            );
        }

        return rows.ToArray();
    }

    public static async Task BattleButtonAsync(Message msg)
    {

        var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData(" ", "11"),
                            InlineKeyboardButton.WithCallbackData("1", "12"),
                            InlineKeyboardButton.WithCallbackData("2", "13"),
                            InlineKeyboardButton.WithCallbackData("3", "14"),
                            InlineKeyboardButton.WithCallbackData("4", "15"),
                            InlineKeyboardButton.WithCallbackData("5", "16"),

                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("6", "21"),
                            InlineKeyboardButton.WithCallbackData("7", "22"),
                            InlineKeyboardButton.WithCallbackData("8", "23"),
                            InlineKeyboardButton.WithCallbackData("9", "24"),
                            InlineKeyboardButton.WithCallbackData("10", "25"),

                        },
                         new []
                        {
                            InlineKeyboardButton.WithCallbackData(" ", "31"),
                            InlineKeyboardButton.WithCallbackData("11", "32"),
                            InlineKeyboardButton.WithCallbackData("12", "33"),
                            InlineKeyboardButton.WithCallbackData("13", "34"),
                            InlineKeyboardButton.WithCallbackData("14", "35"),
                            InlineKeyboardButton.WithCallbackData("15", "36"),

                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("16", "41"),
                            InlineKeyboardButton.WithCallbackData("17", "42"),
                            InlineKeyboardButton.WithCallbackData("18", "43"),
                            InlineKeyboardButton.WithCallbackData("19", "44"),
                            InlineKeyboardButton.WithCallbackData("20", "45"),

                        }
                }
                    );

        ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(
            new KeyboardButton[] { "a", "b", "c"});
        await Bot.SendTextMessageAsync(msg.Chat.Id, "Оберіть шлях:", replyMarkup: inlineKeyboard);
        await Bot.SendTextMessageAsync(msg.Chat.Id, "space", replyMarkup: keyboard);
    }

}

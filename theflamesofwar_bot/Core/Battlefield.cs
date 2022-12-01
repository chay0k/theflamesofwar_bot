
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
public static class Battlefield
{
    public static ITelegramBotClient Bot { get; set; }
    public static string CurrentStatus { get; set; }

    public static async Task UpdateStatusAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string codeOfButton, Models.User user, Table table)
    {
        BattleButtonAsync(update.CallbackQuery.Message);
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
            new KeyboardButton[] { "a", "b", "c" });
        await Bot.SendTextMessageAsync(msg.Chat.Id, "Оберіть шлях:", replyMarkup: inlineKeyboard);
        await Bot.SendTextMessageAsync(msg.Chat.Id, "space", replyMarkup: keyboard);
    }

}

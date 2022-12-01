using System;
using System.Threading;
using System.Threading.Tasks;
using theflamesofwar_bot.Core;
using theflamesofwar_bot.Models;

namespace theflamesofwar_bot
{
    public static class Events
    {
        public static void MessageUpdateAsync(string inputMessage, out string outputMessage, User currentUser)
        {
            outputMessage = "";
            Table currentTable;
            if (currentUser == null)
            {
                currentTable = new Table();
            }
            else 
            {
                currentTable = Lobby.searchActiveTables(currentUser);
            }

            if (inputMessage.StartsWith("/CreateNewUser"))
            {
                outputMessage = "User created";
            }
            else if (inputMessage.StartsWith("/ChooseUser"))
            {
                outputMessage = "User choosed";
            }
            else if (currentUser == null)
            {
                outputMessage = "Need to choise or create user";
            }
            //else if (currentTable == null || currentTable.IsEmpty())
            //{
            //    await Bot.SendTextMessageAsync(message.Chat.Id, "Make you choice", replyMarkup: ButtonsOld.CreateNewMap());
            //    return;
            //}
            //else
            //{
            //    await Bot.SendTextMessageAsync(message.Chat.Id, "Make you choice", replyMarkup: ButtonsOld.LoadMap());
            //}

            //if (message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            //    {
            //        //skip
            //    }
            //    else if (message.Text.ToLower() == "/start")
            //    {
            //        await botClient.SendTextMessageAsync(message.Chat, "Привіт!");
            //        return;
            //    }
            //    else if (message.Text.ToLower() == "/restart")
            //    {
            //        ButtonsOld.CurrentPosition = 1;
            //        //MapGenerator.map.Cells[0, 0].IsOpen = true;
            //        ButtonsOld.MapButtonAsync(message);
            //    }
            //    await botClient.SendTextMessageAsync(message.Chat, "Привіт-привіт!!");
            
            //else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            //{
            //    string codeOfButton = update.CallbackQuery.Data;
            //    ButtonsOld.UpdateStatusAsync(botClient, update, cancellationToken, codeOfButton, currentUser, currentTable);
            //    //Battlefield.UpdateStatusAsync(botClient, update, cancellationToken, codeOfButton, currentUser, currentTable);
            //}

        }

        //public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        //{
        //    // Некоторые действия
        //    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        //}
    }
}

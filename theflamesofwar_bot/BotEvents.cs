using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;
using theflamesofwar_bot.Core;

namespace theflamesofwar_bot
{
    public static class BotEvents
    {

        public static ITelegramBotClient Bot { get; set; }
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var currentUser = Authorisation.CurrentUser(update, Newtonsoft.Json.JsonConvert.SerializeObject(update));
            var currentTable = Lobby.searchActiveTables(currentUser);
            var currentPlayerCondition = 

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {

                var message = update.Message;

                if (currentTable == null || currentTable.IsEmpty())
                {       
                    Bot.SendTextMessageAsync(message.Chat.Id, "Make you choice", replyMarkup: ButtonsOld.CreateNewMap());
                    return;
                }
                else
                {
                    await Bot.SendTextMessageAsync(message.Chat.Id, "Make you choice", replyMarkup: ButtonsOld.LoadMap());
                }

                if (message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
                {
                    //skip
                }
                else if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Привіт!");
                    return;
                }
                else if (message.Text.ToLower() == "/restart")
                {
                    ButtonsOld.CurrentPosition = 1;
                    //MapGenerator.map.Cells[0, 0].IsOpen = true;
                    ButtonsOld.MapButtonAsync(message);
                }
                await botClient.SendTextMessageAsync(message.Chat, "Привіт-привіт!!");
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                string codeOfButton = update.CallbackQuery.Data;
                ButtonsOld.UpdateStatusAsync(botClient, update, cancellationToken, codeOfButton, currentUser, currentTable);
                //Battlefield.UpdateStatusAsync(botClient, update, cancellationToken, codeOfButton, currentUser, currentTable);
            }

        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
    }
}

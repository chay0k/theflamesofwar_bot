using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using theflamesofwar_bot.BotLogic;
using theflamesofwar_bot.Core;

namespace theflamesofwar_bot
{

    public static class BotStart
    {
        static ITelegramBotClient bot = new TelegramBotClient("5710912072:AAE5aujoVmEYL7K7YrlT3GwRxVMw8InDWNc");

        public static void Start()
        {
            Console.WriteLine(bot.GetMeAsync().Result.FirstName + " was started");

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            BotEvents.Bot = bot;
            BotButtonsProcessing.Bot = bot;
            Battlefield.Bot = bot;
            bot.StartReceiving(
                BotEvents.HandleUpdateAsync,
                BotEvents.HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}
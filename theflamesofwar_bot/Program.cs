using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;
using theflamesofwar_bot.Repositories;
using theflamesofwar_bot.Core;


namespace theflamesofwar_bot
{

    class Program
    {

        static void Main(string[] args)
        {
            var clearAllThings = false;
            Console.WriteLine("test");
            //clearAllThings = true;
            Creator.AddBaseLands(clearAllThings);
            Creator.AddBaseResources(clearAllThings);
            Creator.AddBaseThings(clearAllThings);
            BotStart.Start();
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using theflamesofwar_bot.Models;
using theflamesofwar_bot.Repositories;

namespace theflamesofwar_bot.Core;
public static class Authorisation
{
    public static Models.User CurrentUser(Update update, string serObj)
    {
        //539425370
        var userRepository = new SqlUserRepository();

        Chat chat;
        if (update.Message != null)
            chat = update.Message.Chat;
        else if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
            chat = update.CallbackQuery.Message.Chat;
        else
        {
            Console.WriteLine("Message exception. Can't find chat ID");
            return null;
        }
        var users = userRepository.SearchByTelegramId(chat.Id);
        if (users == null || users.Count == 0 )
            return CreateUser(chat, userRepository);
        else
            return users[0];
    }
    private static Models.User CreateUser(Chat chat, SqlUserRepository userRepository)
    {
        var user = new Models.User();
        user.TelegramId = chat.Id;
        user.Name = chat.Username;
        user.FirstName = chat.FirstName;
        user.LastName = chat.LastName;
        user.TelegramUserName = chat.Username;
        userRepository.Create(user);
        userRepository.Save();
        return user;

    }


}

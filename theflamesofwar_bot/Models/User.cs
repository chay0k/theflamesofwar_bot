using System;
using System.Collections.Generic;
using System.Text;

namespace theflamesofwar_bot.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long TelegramId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelegramUserName { get; set; }
        public bool IsEmpty()
        {
            return Name == "";
        }

    }
}

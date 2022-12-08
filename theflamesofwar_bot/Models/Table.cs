using System;
using System.Collections.Generic;
using System.Text;
using theflamesofwar_bot.Repositories;

namespace theflamesofwar_bot.Models
{
    public class Table
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Token { get; set; }
        public Guid MapId { get; set; }
        public Map Map { get; set; }
        public bool Open { get; set; }
        public List<User> Users { get; set; }
        public bool IsEmpty()
        {
            return Name == "";
        }

    }
}

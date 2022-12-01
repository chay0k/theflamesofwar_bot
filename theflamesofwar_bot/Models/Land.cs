using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theflamesofwar_bot.Models
{
    public class Land
    {
        public Guid Id { get; set; }
        public int CardNumber { get; set; } = 0;
        public string Name { get; set; } = "";
        public int AccessLevel { get; set; } = 0;
        public Passabilities Passability { get; set; }
        public Guid PassabilityOption { get; set; }
        public int Steps { get; set; }
        public string Emoji { get; set; }
        public Land()
        {
        }
    }

    public enum Passabilities
    {
        Possible,
        Impossible,
        Optional
    }

}
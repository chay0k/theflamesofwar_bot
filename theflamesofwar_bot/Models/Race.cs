using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theflamesofwar_bot.Models
{
    public class Race
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public List<Unit> Unit { get; set; } = new List<Unit>();
    }
}
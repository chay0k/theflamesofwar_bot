using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theflamesofwar_bot.Models;
public class GameLog
{
    public Guid Id { get; set; }
    public User Player { get; set; }
    public string Operatin { get; set; }
    public string Description { get; set; }
    DateTime DateTime { get; set; }
}

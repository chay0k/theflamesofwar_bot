using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theflamesofwar_bot.Models;

namespace theflamesofwar_bot.Core;
public class Action
{
    //public Table Table { get; set; }
    //public List<User> Users { get; set; }
    delegate void Message();

    public Action (IActioned platform)
    {
        var user = platform.GetCurrentUser();
    }
}

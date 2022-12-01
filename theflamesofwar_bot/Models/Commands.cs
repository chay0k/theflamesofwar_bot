using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theflamesofwar_bot.Models;

public struct Commands
{
    string Name;
    delegate void function();
}

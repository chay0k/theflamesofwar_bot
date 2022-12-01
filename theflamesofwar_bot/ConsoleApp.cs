using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theflamesofwar_bot;
public static class ConsoleApp
{
    public static string consoleCommand = "";
    public static void Start()
    {
        Console.WriteLine("Console bot was started");
        while(consoleCommand != "QUIT")
        {
            consoleCommand = Console.ReadLine();
        }
    }
}

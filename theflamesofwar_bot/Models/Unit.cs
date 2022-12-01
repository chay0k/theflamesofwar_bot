using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theflamesofwar_bot.Models
{

    public enum UnitTypes
    {
        Scout,
        Ranged,
        Defender,
        Wizzard,
        ScrollOfMagic
    }

    public class Unit : Thing
    {
        public Guid Id { get; set; }
//        public int Level { get; set; }
        public int Speed { get; set; }
        public int Attack { get; set; }
        public int MaxHP { get; set; }
        public int EnergyCost { get; set; }
        public int Weight { get; set; }
        public UnitTypes UnitType { get; set; }
        public int ExstraEnergy { get; set; }
        public Race Race { get; set; } = new Race();
        public Unit()
        {

        }

    }
}
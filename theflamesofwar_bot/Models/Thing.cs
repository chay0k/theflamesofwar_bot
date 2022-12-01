using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theflamesofwar_bot.Models
{
    public class Thing
    {
        public Guid Id { get; set; }
        //public Guid ThingId { get; set; }
        public string Name { get; set; } = "";
        public int Count { get; set; } = 0;
        //public ThingDetails Details { get; set; }
        public string ThingType { get; set; }
        //public Unit Unit { get; set; }
        public Guid ResourceId { get; set; }
        public Resource Resource { get; set; }
        public string Emoji { get; set; }
    }

    public enum ThingTypes
    {
        Castle,
        Hero,
        Unit,
        Resource,
        Artifact,
        Nothing
    }
}
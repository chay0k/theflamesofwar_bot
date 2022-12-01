using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theflamesofwar_bot.Models
{
    public class Cell
    {
        public Guid Id { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        [NotMapped]
        public Thing Thing { get; set; }
        public Guid ThingId { get; set; }
        [NotMapped]
        public Land Land { get; set; }
        public Guid LandId { get; set; }
        public Guid MapID { get; set; }
        [NotMapped]
        public Map Map { get; set; } 

        public bool IsOpen { get; set; } = false;

    }
}
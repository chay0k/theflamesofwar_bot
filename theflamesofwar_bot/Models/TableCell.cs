using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theflamesofwar_bot.Models
{
    public class TableCell
    {
        public Guid Id { get; set; }
        public Guid CellId { get; set; }
        [NotMapped]
        public Cell Cell { get; set; }
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
        public Guid TableId { get; set; }
        public bool IsOpen { get; set; }
        public int PlayerPosition { get; set; }
        public TableCell()
        {
            IsOpen = false;
            PlayerPosition = 0;
        }
        public TableCell(Cell cell)
        {
            FillFromCell(cell);
            IsOpen = false;
            PlayerPosition = 0;
        }
        public TableCell(Cell cell, Guid tableId)
        {
            TableId = tableId;
            FillFromCell(cell);
            IsOpen = false;
            PlayerPosition = 0;
        }

        private void FillFromCell(Cell cell)
        {
            Cell = cell;
            CellId = cell.Id;
            ThingId = cell.ThingId;
            Thing = cell.Thing;
            MapID = cell.MapID;
            Map = cell.Map;
            CoordinateX = cell.CoordinateX;
            CoordinateY = cell.CoordinateY;
            Land = cell.Land;
            LandId = cell.LandId;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theflamesofwar_bot.Repositories;

namespace theflamesofwar_bot.Models
{

    public class Map
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Players { get; set; }
        public int SizeX { get; } = 0;
        public int SizeY { get; } = 0;
        [NotMapped]
        public Cell[,] Cells { get; set; } = new Cell[0, 0];
        public Map()
        {
            SizeX = 15;
            SizeY = 10;
            CreateCells();
        }


        public Map(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            CreateCells();
        }

        private void CreateCells()
        {
            Cells = new Cell[SizeX, SizeY];
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                {
                    Cells[i, j] = new Cell();
                    Cells[i, j].CoordinateX = i;
                    Cells[i, j].CoordinateY = j;
                    Cells[i, j].Map = this;
                }
        }

        public void Print(string field)
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    if (field.ToLower() == "land")
                        Console.Write($"{Cells[i, j].Land.Name} \t");
                    else if (field.ToLower() == "thing")
                        if (Cells[i, j].Thing == Creator.emptyThing)
                            Console.Write(" \t");
                        else
                            Console.Write($"{Cells[i, j].Thing.Name} \t");
                }
                Console.WriteLine();
            }
        }
    }
}
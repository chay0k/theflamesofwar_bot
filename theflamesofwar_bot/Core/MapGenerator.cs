using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theflamesofwar_bot.Models;
using theflamesofwar_bot.Repositories;

namespace theflamesofwar_bot.Core
{
    
    public static class MapGenerator
    {
        public static Map map = new Map();
        public static void CreateRandomMap()
        {
            int countPlayers = 2;

            map = new Map();
            map.Id = Guid.NewGuid();
            map.Players = countPlayers;
            var sqlMap = new SqlMapRepository();
            map.Name = $"Random map #{sqlMap.GetList().Count()}";
            sqlMap.Create(map);
            sqlMap.Save();
            var cellRepository = new SqlCellRepository();
            var thingRepository = new SqlThingRepository();
            var landRepository = new SqlLandRepository();
            var resourceRepository = new SqlResourceRepository();
            var lands = landRepository.GetList();
            Console.WriteLine("Land list:");
            foreach (Land u in lands)
            {
                Console.WriteLine($"{u.Id}.{u.Name} - {u.Passability}, {u.CardNumber}");
            }
            var landList = lands.ToList();
            var resources = resourceRepository.GetList();
            Console.WriteLine("Resource list:");
            foreach (Resource u in resources)
            {
                Console.WriteLine($"{u.Id} - {u.Name}");
            }
            var resourcesList = resources.ToList();
            var rnd = new Random();
            Console.WriteLine();
            Console.WriteLine("Map:");
            for (int i = 0; i < map.SizeX; i++)
            {
                for (int j = 0; j < map.SizeY; j++)
                {
                    var randomLandID = rnd.Next(landList.Count);
                    var randomResourceID = rnd.Next(resourcesList.Count);
                    var random10 = rnd.Next(10);
                    var cell = new Cell();
                    var resource = Creator.emptyResource;
                    cell.CoordinateX = i;
                    cell.CoordinateY = j;
                    var land = landList[randomLandID];
                    var thing = new Thing();
                    cell.Id = Guid.NewGuid();
                    cell.LandId = land.Id;
                    //cell.IsOpen = false;
                    cell.MapID = map.Id;
                    if (i == 0 && j == 0)
                        cell.PlayerPosition = 1;
                    else if (i == map.SizeX-1 && j == map.SizeY-1)
                        cell.PlayerPosition = 2;
                    if (random10 == 0 && land.Passability == Passabilities.Possible)
                    {
                        resource = resourcesList[randomResourceID];
                        thing.Id = Guid.NewGuid();
                        thing.ThingType = "Resource";
                        thing.Name = resource.Name;
                        thing.ResourceId = resource.Id;
                        //thing.ThingId = resource.Id;
                        thing.Emoji = resource.Emoji;
                        thingRepository.Create(thing);
                        thingRepository.Save();
                        thing.Resource = resource;
                    }
                    else
                    {
                        thing = Creator.emptyThing;
                    }
                    cell.ThingId = thing.Id;
                    cellRepository.Create(cell);
                    cellRepository.Save();
                    cell.Map = map;
                    cell.Thing = thing;
                    cell.Land = landList[randomLandID];
                    map.Cells[i, j] = cell;
                }
                
            }
            

            map.Print("land");
            map.Print("thing");
        }

        public static void RestoreMap(Map currentMap)
        {
            var cellRep = new SqlCellRepository();
            var landRep = new SqlLandRepository();
            var thingRep = new SqlThingRepository();
            for (int i = 0; i < currentMap.SizeX; i++)
            {
                for (int j = 0; j < currentMap.SizeY; j++)
                {
                    var cell = cellRep.Get(currentMap.Id, i, j);
                    cell.Land = landRep.Get(cell.LandId);
                    if (cell.ThingId == Creator.emptyThing.Id)
                        cell.Thing = Creator.emptyThing;
                    else
                        cell.Thing = thingRep.Get(cell.ThingId);

                    currentMap.Cells[i, j] = cell;
                }
            }
        }
    }
}
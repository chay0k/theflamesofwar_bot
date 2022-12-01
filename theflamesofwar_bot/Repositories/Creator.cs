using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using theflamesofwar_bot.Contexts;
using theflamesofwar_bot.Models;

namespace theflamesofwar_bot.Repositories
{
    public static class Creator
    {
        public static Resource emptyResource;
        public static Thing emptyThing;
        public static void AddBaseLands(bool clearPrevious = false) 
        {
            var landRepository = new SqlLandRepository();
            if (clearPrevious)
                landRepository.ClearAsync();
            else
            {
                var landlist = landRepository.GetList();
                if (landlist != null)
                    foreach (var land in landlist)
                    {
                        Console.WriteLine("Lands are exist");
                        return;
                    }
            }
            Land dirt = new Land { Name = "Dirt", AccessLevel = 1, Passability = Passabilities.Possible, Steps = 4, CardNumber = 1, Emoji = "🏿" };
            Land road = new Land { Name = "Road", AccessLevel = 1, Passability = Passabilities.Possible, Steps = 2, CardNumber = 2, Emoji = char.ConvertFromUtf32(0x1F6E3) };
            Land mount = new Land { Name = "Mount", AccessLevel = 1, Passability = Passabilities.Impossible, CardNumber = 4, Emoji = char.ConvertFromUtf32(0x26F0) };

            landRepository.Create(dirt);
            landRepository.Create(road);
            landRepository.Create(mount);
            landRepository.Save();
            Console.WriteLine("Lands saved successfully");
        }
        public static void AddBaseThings(bool clearPrevious = false)
        {
            var thingRepository = new SqlThingRepository();

            if (clearPrevious)
                thingRepository.ClearAsync();
            else
            {
                var thinglist = thingRepository.GetList();
                if (thinglist != null)
                    foreach (var thing in thinglist)
                        if (thing.Name == "Empty")
                        {
                            Console.WriteLine("Things are exist");
                            emptyThing = thing;
                            return;
                        }
            }

            emptyThing = new Thing { Id = Guid.NewGuid(), Name = "Empty", Emoji = " ", Count = 0, ThingType = "Nothing", ResourceId = emptyResource.Id };

            thingRepository.Create(emptyThing);
            thingRepository.Save();
            Console.WriteLine("Nothing saved successfully");
        }
        public static void AddBaseResources(bool clearPrevious = false)
        {
            var resourceRepository = new SqlResourceRepository();

            if (clearPrevious)
                resourceRepository.ClearAsync();
            else
            {
                var resourcelist = resourceRepository.GetFullList();
                if (resourcelist != null)
                    foreach (var resource in resourcelist)
                    {
                        if (resource.Name == "Empty")
                        {
                            Console.WriteLine("Resources are exist");
                            emptyResource = resource;
                            return;
                        }
                    }
            }
            emptyResource = new Resource { Name = "Empty", Emoji = "" };
            Resource gold = new Resource { Name = "Money", Emoji = "💰" };
            Resource stone = new Resource { Name = "Stone", Emoji = "🪨" };
            Resource wood = new Resource { Name = "Wood", Emoji = "🪵" };
            Resource steel = new Resource { Name = "Steel", Emoji = "🔗" };

            resourceRepository.Create(emptyResource);
            resourceRepository.Create(gold);
            resourceRepository.Create(stone);
            resourceRepository.Create(wood);
            resourceRepository.Create(steel);
            resourceRepository.Save();
            Console.WriteLine("Resource saved successfully");
        }
        
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using theflamesofwar_bot.Contexts;
using theflamesofwar_bot.Models;
using System.Linq;

namespace theflamesofwar_bot.Repositories
{
    public class SqlThingRepository : IRepository<Thing>
    {
        
    private GameContext db;

        public SqlThingRepository()
        {
            this.db = new GameContext();
        }

        public IEnumerable<Thing> GetList()
        {
            return db.Things;
        }

        public Thing Get(Guid id)
        {
            return db.Things.Find(id);
        }


        public void Create(Thing thing)
        {
            db.Things.Add(thing);
        }

        public void Update(Thing thing)
        {
            db.Entry(thing).State = EntityState.Modified;
        }

        public void Delete(Thing thing)
        {
            if (thing != null)
                db.Things.Remove(thing);
        }

        public void Delete(int id)
        {
            var thing = db.Things.Find(id);
            Delete(thing);
        }

        public async Task ClearAsync()
        {
            var allThings = await db.Things.ToListAsync();
            foreach (var thing in allThings)
                Delete(thing);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}


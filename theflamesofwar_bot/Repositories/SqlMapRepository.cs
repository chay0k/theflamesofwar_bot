using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using theflamesofwar_bot.Contexts;
using theflamesofwar_bot.Models;

namespace theflamesofwar_bot.Repositories
{
    public class SqlMapRepository : IRepository<Map>
    {
        
    private GameContext db;

        public SqlMapRepository()
        {
            this.db = new GameContext();
        }

        public IEnumerable<Map> GetList()
        {
            return db.Maps;
        }

        public Map Get(Guid id)
        {
            return db.Maps.Find(id);
        }

        public void Create(Map map)
        {
            db.Maps.Add(map);
        }

        public void Update(Map map)
        {
            db.Entry(map).State = EntityState.Modified;
        }

        public void Delete(Map map)
        {
            if (map != null)
                db.Maps.Remove(map);
        }

        public void Delete(int id)
        {
            var map = db.Maps.Find(id);
            Delete(map);
        }

        public async Task ClearAsync()
        {
            var allMaps = await db.Maps.ToListAsync();
            foreach (var map in allMaps)
                Delete(map);
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


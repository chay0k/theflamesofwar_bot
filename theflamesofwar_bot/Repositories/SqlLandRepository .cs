using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using theflamesofwar_bot.Contexts;
using theflamesofwar_bot.Models;

namespace theflamesofwar_bot.Repositories
{
    public class SqlLandRepository : IRepository<Land>
    {
        
    private GameContext db;

        public SqlLandRepository()
        {
            this.db = new GameContext();
        }

        public IEnumerable<Land> GetList()
        {
            return db.Lands;
        }

        public Land Get(Guid id)
        {
            return db.Lands.Find(id);
        }

        public void Create(Land land)
        {
            db.Lands.Add(land);
        }

        public void Update(Land land)
        {
            db.Entry(land).State = EntityState.Modified;
        }

        public void Delete(Land land)
        {
            if (land != null)
                db.Lands.Remove(land);
        }

        public void Delete(int id)
        {
            Land land = db.Lands.Find(id);
            Delete(land);
        }

        public async Task ClearAsync()
        {
            var allLands = await db.Lands.ToListAsync();
            foreach (var land in allLands)
                Delete(land);
            Save();
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


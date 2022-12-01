using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using theflamesofwar_bot.Contexts;
using theflamesofwar_bot.Models;

namespace theflamesofwar_bot.Repositories
{
    public class SqlResourceRepository : IRepository<Resource>
    {
        
    private GameContext db;

        public SqlResourceRepository()
        {
            this.db = new GameContext();
        }

        public IEnumerable<Resource> GetList()
        {
            var list = db.Resources;
            list.Remove(Creator.emptyResource);
            return list;
        }
        public IEnumerable<Resource> GetFullList()
        {
            var list = db.Resources;
            return list;
        }
        public Resource Get(Guid id)
        {
            return db.Resources.Find(id);
        }

        public void Create(Resource resource)
        {
            db.Resources.Add(resource);
        }

        public void Update(Resource resource)
        {
            db.Entry(resource).State = EntityState.Modified;
        }

        public void Delete(Resource resource)
        {
            if (resource != null)
                db.Resources.Remove(resource);
        }

        public void Delete(int id)
        {
            Resource resource = db.Resources.Find(id);
            Delete(resource);
        }
        public async Task ClearAsync()
        {
            var allResource = await db.Resources.ToListAsync();
            foreach (var resource in allResource)
                Delete(resource);
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


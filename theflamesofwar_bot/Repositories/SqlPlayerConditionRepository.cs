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
    public class SqlPlayerConditionRepository : IRepository<PlayerCondition>
    {
        
    private GameContext db;

        public SqlPlayerConditionRepository()
        {
            this.db = new GameContext();
        }

        public IEnumerable<PlayerCondition> GetList()
        {
            return db.PlayerConditions;
        }

        public PlayerCondition Get(Guid id)
        {
            return db.PlayerConditions.Find(id);
        }

        public void Create(PlayerCondition playerCondition)
        {
            db.PlayerConditions.Add(playerCondition);
        }

        public void Update(PlayerCondition playerCondition)
        {
            db.Entry(playerCondition).State = EntityState.Modified;
        }

        public void Delete(PlayerCondition playerCondition)
        {
            if (playerCondition != null)
                db.PlayerConditions.Remove(playerCondition);
        }

        public void Delete(int id)
        {
            var playerCondition = db.PlayerConditions.Find(id);
            Delete(playerCondition);
        }

        public async Task ClearAsync()
        {
            var allPlayerConditions = await db.PlayerConditions.ToListAsync();
            foreach (var playerCondition in allPlayerConditions)
                Delete(playerCondition);
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


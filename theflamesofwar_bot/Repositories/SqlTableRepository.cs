using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using theflamesofwar_bot.Contexts;
using theflamesofwar_bot.Models;

namespace theflamesofwar_bot.Repositories
{
    public class SqlTableRepository : IRepository<Table>
    {
        
    private GameContext db;

        public SqlTableRepository()
        {
            this.db = new GameContext();
        }

        public IEnumerable<Table> GetList()
        {
            return db.Tables;
        }

        public Table Get(Guid id)
        {
            return db.Tables.Find(id);
        }

        public void Create(Table table)
        {
            db.Tables.Add(table);
        }

        public void Update(Table table)
        {
            db.Entry(table).State = EntityState.Modified;
        }

        public void Delete(Table table)
        {
            if (table != null)
                db.Tables.Remove(table);
        }

        public void Delete(int id)
        {
            var table = db.Tables.Find(id);
            Delete(table);
        }

        public async Task ClearAsync()
        {
            var allTables = await db.Tables.ToListAsync();
            foreach (var table in allTables)
                Delete(table);
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


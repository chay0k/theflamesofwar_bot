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
    public class SqlCellRepository : IRepository<Cell>
    {
        
    private GameContext db;

        public SqlCellRepository()
        {
            this.db = new GameContext();
        }

        public IEnumerable<Cell> GetList()
        {
            return db.Cells;
        }

        public Cell Get(Guid id)
        {
            return db.Cells.Find(id);
        }

        public Cell Get(Guid mapId, int X, int Y)
        {
            var cellsList = (from cells in db.Cells
                                where cells.MapID == mapId && cells.CoordinateX == X && cells.CoordinateY == Y
                                
                                select cells).ToList();

            if (cellsList.Count == 1)
                return cellsList[0];
            else return null;
        }

        public void Create(Cell cell)
        {
            db.Cells.Add(cell);
        }

        public void Update(Cell cell)
        {
            db.Entry(cell).State = EntityState.Modified;
        }

        public void Delete(Cell cell)
        {
            if (cell != null)
                db.Cells.Remove(cell);
        }

        public void Delete(int id)
        {
            var cell = db.Cells.Find(id);
            Delete(cell);
        }

        public async Task ClearAsync()
        {
            var allCells = await db.Cells.ToListAsync();
            foreach (var cell in allCells)
                Delete(cell);
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


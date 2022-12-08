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
    public class SqlTableCellRepository : IRepository<TableCell>
    {
        
    private GameContext db;

        public SqlTableCellRepository()
        {
            this.db = new GameContext();
        }

        public IEnumerable<TableCell> GetList()
        {
            return db.TableCells;
        }

        public TableCell Get(Guid id)
        {
            return db.TableCells.Find(id);
        }

        public TableCell Get(Table Table, int X, int Y)
        {

            var tableCellsQery = (from tableCells in db.TableCells
                             where 
                             tableCells.MapID == Table.MapId && 
                                   tableCells.TableId == Table.Id && 
                                   tableCells.CoordinateX == X && 
                                   tableCells.CoordinateY == Y
                                select tableCells);
            var tableCellsList = tableCellsQery.ToList();

            if (tableCellsList.Count == 1)
                return tableCellsList[0];
            else return null;
        }

        public void Create(TableCell tableCell)
        {
            db.TableCells.Add(tableCell);
        }

        public void Update(TableCell tableCell)
        {
            db.Entry(tableCell).State = EntityState.Modified;
        }

        public void Delete(TableCell tableCell)
        {
            if (tableCell != null)
                db.TableCells.Remove(tableCell);
        }

        public void Delete(int id)
        {
            var tableCell = db.TableCells.Find(id);
            Delete(tableCell);
        }

        public async Task ClearAsync()
        {
            var allTableCells = await db.TableCells.ToListAsync();
            foreach (var tableCell in allTableCells)
                Delete(tableCell);
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


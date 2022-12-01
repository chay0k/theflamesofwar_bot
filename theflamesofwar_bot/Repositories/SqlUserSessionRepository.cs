using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theflamesofwar_bot.Contexts;
using theflamesofwar_bot.Models;

namespace theflamesofwar_bot.Repositories
{
    public class SqlUserSessionRepository : IRepository<UserSession>
    {
        
    private GameContext db;

        public SqlUserSessionRepository()
        {
            this.db = new GameContext();
        }

        public IEnumerable<UserSession> GetList()
        {
            return db.UserSessions;
        }

        public UserSession Get(Guid id)
        {
            return db.UserSessions.Find(id);
        }

        public UserSession SearchByUser(User user)
        {
            var userSessions = (from userSession in db.UserSessions
                                where userSession.PlayerId == user.Id
                                orderby userSession.DateTime descending
                                select userSession).ToList();
            return userSessions.FirstOrDefault();
        }

        public List<Table> SearchTablesByUser(User user)
        {
            var tablesIds = (from userSession in db.UserSessions
                                where userSession.PlayerId == user.Id
                                orderby userSession.DateTime descending
                                select userSession.TableId).ToList();
            var sqlTableRep = new SqlTableRepository();
            var tables = new List<Table>();
            foreach (var tableId in tablesIds)
                tables.Add(sqlTableRep.Get(tableId));
            return tables;
        }

        public void Create(UserSession userSession)
        {
            db.UserSessions.Add(userSession);
        }

        public void Update(UserSession userSession)
        {
            db.Entry(userSession).State = EntityState.Modified;
        }

        public void Delete(UserSession userSession)
        {
            if (userSession != null)
                db.UserSessions.Remove(userSession);
        }

        public void Delete(int id)
        {
            var userSession = db.UserSessions.Find(id);
            Delete(userSession);
        }

        public async Task ClearAsync()
        {
            var allSessions = await db.UserSessions.ToListAsync();
            foreach (var userSession in allSessions)
                Delete(userSession);
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


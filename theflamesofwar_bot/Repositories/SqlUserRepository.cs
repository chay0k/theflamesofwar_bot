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
    public class SqlUserRepository : IRepository<User>
    {
        
    private GameContext db;

        public SqlUserRepository()
        {
            this.db = new GameContext();
        }

        public IEnumerable<User> GetList()
        {
            return db.Users;
        }

        public User Get(Guid id)
        {
            return db.Users.Find(id);
        }

        public List<User> SearchByTelegramId(long TelegramId)
        {
            var users = db.Users.Where(p => p.TelegramId == TelegramId).ToList();
            return users;
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(User user)
        {
            if (user != null)
                db.Users.Remove(user);
        }

        public void Delete(int id)
        {
            var user = db.Users.Find(id);
            Delete(user);
        }

        public async Task ClearAsync()
        {
            var allUsers = await db.Users.ToListAsync();
            foreach (var user in allUsers)
                Delete(user);
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


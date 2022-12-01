using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using theflamesofwar_bot.Models;

namespace theflamesofwar_bot.Contexts

{
    public class GameContext : DbContext
    {
        public DbSet<PlayerCondition> PlayerConditions { get; set; }
        public DbSet<Land> Lands { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Thing> Things { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<GameLog> GameLog { get; set; }

        //public GameContext() => Database.EnsureDeleted();
        public GameContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=theflamesofwar.db");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Land>();
        //    //modelBuilder.Entity<Resource>();
        //    //modelBuilder.Entity<Thing>();
        //    //modelBuilder.Entity<Map>();
        //    //modelBuilder.Entity<Table>();
        //    //modelBuilder.Entity<User>();
        //    //modelBuilder.Entity<UserSession>();
        //    //modelBuilder.Entity<GameLog>();
        //}
    }
}

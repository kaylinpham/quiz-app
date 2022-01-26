using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
namespace server.Models {
    public class QuizContext : DbContext {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options){
            
        }
        public DbSet<QuizInfo> QuizInfos {get; set;}
        public DbSet<User> Users {get; set;}
        public DbSet<QuizPackage> QuizPackages {get; set;}
        protected override void OnModelCreating (ModelBuilder builder){
            base.OnModelCreating(builder);
            builder.Entity<QuizPackage>().HasKey(c => new {c.PackageID, c.UserID});
        }
    }
}
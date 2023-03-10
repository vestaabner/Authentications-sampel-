using System;
using BackOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BackOffice.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<User, Roles, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<Product>? Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ggg.Db;Uid=sa;Pwd=yourStrong(!)Password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {

        AppDbContext IDesignTimeDbContextFactory<AppDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ggg.Db;Uid=sa;Pwd=yourStrong(!)Password");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}


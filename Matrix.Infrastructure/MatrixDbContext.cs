using System;
using Matrix.Core;
using Matrix.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Matrix.Infrastructur
{
	
    public class MatrixDbContext : DbContext
    {
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Feature> Features { get; set; }
        public DbSet<Post> Products { get; set; }
        public DbSet<User> Users { get; set; }


        public MatrixDbContext(DbContextOptions<MatrixDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MatrixDbContext).Assembly);
        }

        
    }
    //public class AppDbContextDesignTimeFactory : IDesignTimeDbContextFactory<MatrixDbContext>
    //{
    //    const string SqliteDbConnection = "Data Source=EFCorePractice.db;Cache=Shared";
    //    const string SqlServerDbConnection = "data source =.; Initial Catalog = AhmadMessages; user id = sa; password = Aa123456;";

    //    public MatrixDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<MatrixDbContext>();
    //        //optionsBuilder.UseSqlite(args.Length > 0 ? args[0] : SqliteDbConnection, a => a.MigrationsAssembly(GetType().Assembly.FullName));
    //        optionsBuilder.UseSqlServer(args.Length > 0 ? args[0] : SqlServerDbConnection, a => a.MigrationsAssembly(GetType().Assembly.FullName));
    //        return new MatrixDbContext(optionsBuilder.Options);
    //    }
    //}
}


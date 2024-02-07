using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataAccessLib.Persistence.Context
{
    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext(DbContextOptions options)
            : base(options)
        {
        }

        // Used by Dapper

        public IDbConnection Connection => Database.GetDbConnection();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
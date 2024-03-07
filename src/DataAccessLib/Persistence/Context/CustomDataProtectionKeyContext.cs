using System;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLib.Persistence.Context
{
	public class CustomDataProtectionKeyContext : BaseDbContext, IDataProtectionKeyContext
    {
		public CustomDataProtectionKeyContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dist_cache");
        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

    }
}


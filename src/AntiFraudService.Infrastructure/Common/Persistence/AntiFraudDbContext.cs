using AntiFraudService.Domain.AntiFraud;
using Microsoft.EntityFrameworkCore;
using System;

namespace AntiFraudService.Infrastructure.Common.Persistence
{
    public class AntiFraudDbContext: DbContext
    {
        public AntiFraudDbContext(DbContextOptions<AntiFraudDbContext> options) : base(options)
        {
        }

        public DbSet<AntiFraud> AntiFrauds  { get; set; }

        public async Task CommitChangesAsync()
        {
            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AntiFraud>()
                .ToTable("antifrauds")
                .HasKey(a => a.Id); // Define the primary key explicitly; // Ensure correct table name
        }
    }   
}

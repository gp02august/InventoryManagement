using InventoryService.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<InventoryItem> InventoryItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.Property(x => x.CreatedAt)
                    .HasColumnType("timestamp with time zone");

                entity.Property(x => x.UpdatedAt)
                    .HasColumnType("timestamp with time zone");
            });
        }
    }
}
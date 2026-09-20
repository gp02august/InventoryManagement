using Microsoft.EntityFrameworkCore;
using ProductService.Entities;

namespace ProductService.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(
            DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(x => x.Price)
                    .HasPrecision(10, 2);

                entity.Property(x => x.CreatedAt)
                    .HasColumnType("timestamp with time zone");

                entity.Property(x => x.UpdatedAt)
                    .HasColumnType("timestamp with time zone");
            });
        }
    }
}
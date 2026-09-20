using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Entities;
using ProductService.Repository.Interfaces;

namespace ProductService.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> GetByIdAsync(Guid productId)
        {
            return await _context.Products
                .FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<List<Product>> GetAllAsync(
            int pageNumber,
            int pageSize)
        {
            return await _context.Products
                .Where(x => x.IsActive)
                .OrderBy(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Products
                .CountAsync(x => x.IsActive);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);

            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(Product product)
        {
            product.IsActive = false;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ReduceStockAsync(
            Guid productId,
            int quantity)
        {
            var affectedRows = await _context.Products
                .Where(x =>
                    x.ProductId == productId &&
                    x.IsActive &&
                    x.StockQty >= quantity)
                .ExecuteUpdateAsync(setters =>
                    setters.SetProperty(
                        x => x.StockQty,
                        x => x.StockQty - quantity)
                );

            return affectedRows > 0;
        }
    }
}
using ProductService.Entities;

namespace ProductService.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);

        Task<Product?> GetByIdAsync(Guid productId);

        Task<List<Product>> GetAllAsync(
            int pageNumber,
            int pageSize);

        Task<int> GetTotalCountAsync();

        Task UpdateAsync(Product product);

        Task SoftDeleteAsync(Product product);

        Task<bool> ReduceStockAsync(
            Guid productId,
            int quantity);
    }
}
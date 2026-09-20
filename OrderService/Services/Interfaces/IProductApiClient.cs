using OrderService.DTO;

namespace OrderService.Services.Interfaces
{
    public interface IProductApiClient
    {
        Task<ProductServiceResponseDto?> GetProductAsync(
            Guid productId);

        Task<bool> ReduceStockAsync(
            Guid productId,
            int quantity);
    }
}
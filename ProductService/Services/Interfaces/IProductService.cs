using ProductService.DTO;

namespace ProductService.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseDto> CreateAsync(CreateProductDto dto);

        Task<ProductResponseDto?> GetByIdAsync(Guid productId);

        Task<PagedProductResponseDto> GetAllAsync(
            int pageNumber,
            int pageSize);

        Task<ProductResponseDto?> UpdateAsync(
            Guid productId,
            UpdateProductDto dto);

        Task<bool> SoftDeleteAsync(Guid productId);

        Task<bool> ReduceStockAsync(
            Guid productId,
            int quantity);
    }
}
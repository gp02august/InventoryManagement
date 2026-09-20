using ProductService.DTO;
using ProductService.Entities;
using ProductService.Repository.Interfaces;
using ProductService.Services.Interfaces;

namespace ProductService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                StockQty = dto.StockQty,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var createdProduct = await _productRepository.CreateAsync(product);

            return MapToResponseDto(createdProduct);
        }

        public async Task<ProductResponseDto?> GetByIdAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null || !product.IsActive)
            {
                return null;
            }

            return MapToResponseDto(product);
        }

        public async Task<PagedProductResponseDto> GetAllAsync(
            int pageNumber,
            int pageSize)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            var products = await _productRepository
                .GetAllAsync(pageNumber, pageSize);

            var totalItems = await _productRepository
                .GetTotalCountAsync();

            var totalPages = (int)Math.Ceiling(
                totalItems / (double)pageSize);

            return new PagedProductResponseDto
            {
                Items = products
                    .Select(MapToResponseDto)
                    .ToList(),

                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<ProductResponseDto?> UpdateAsync(
            Guid productId,
            UpdateProductDto dto)
        {
            var product = await _productRepository
                .GetByIdAsync(productId);

            if (product == null || !product.IsActive)
            {
                return null;
            }

            product.ProductName = dto.ProductName;
            product.Price = dto.Price;
            product.StockQty = dto.StockQty;
            product.UpdatedAt = DateTime.UtcNow;

            await _productRepository.UpdateAsync(product);

            return MapToResponseDto(product);
        }

        public async Task<bool> SoftDeleteAsync(Guid productId)
        {
            var product = await _productRepository
                .GetByIdAsync(productId);

            if (product == null || !product.IsActive)
            {
                return false;
            }

            await _productRepository.SoftDeleteAsync(product);

            return true;
        }

        public async Task<bool> ReduceStockAsync(
            Guid productId,
            int quantity)
        {
            if (quantity <= 0)
            {
                return false;
            }

            return await _productRepository
                .ReduceStockAsync(productId, quantity);
        }

        private static ProductResponseDto MapToResponseDto(
            Product product)
        {
            return new ProductResponseDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                StockQty = product.StockQty,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}
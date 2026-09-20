using OrderService.DTO;
using OrderService.Services.Interfaces;
using System.Net;
using System.Net.Http.Json;

namespace OrderService.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductApiClient> _logger;

        public ProductApiClient(
            HttpClient httpClient,
            ILogger<ProductApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ProductServiceResponseDto?> GetProductAsync(
            Guid productId)
        {
            _logger.LogInformation(
                "Calling ProductService to get product {ProductId}",
                productId);

            var response = await _httpClient.GetAsync(
                $"Products/{productId}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogWarning(
                    "Product {ProductId} was not found.",
                    productId);

                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<ProductServiceResponseDto>();
        }

        public async Task<bool> ReduceStockAsync(
            Guid productId,
            int quantity)
        {
            _logger.LogInformation(
                "Calling ProductService to reduce stock. " +
                "ProductId: {ProductId}, Quantity: {Quantity}",
                productId,
                quantity);

            var response = await _httpClient.PostAsJsonAsync(
                $"Products/{productId}/reduce-stock",
                new
                {
                    quantity
                });

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                _logger.LogWarning(
                    "Stock reduction failed for product {ProductId}. " +
                    "Quantity: {Quantity}",
                    productId,
                    quantity);

                return false;
            }

            response.EnsureSuccessStatusCode();

            return true;
        }
    }
}
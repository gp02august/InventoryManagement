using OrderService.DTO;
using OrderService.Entities;
using OrderService.Repository.Interfaces;
using OrderService.Services.Interfaces;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductApiClient _productApiClient;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            IOrderRepository orderRepository,
            IProductApiClient productApiClient,
            ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _productApiClient = productApiClient;
            _logger = logger;
        }

        public async Task<OrderResponseDto> CreateAsync(
            CreateOrderDto dto)
        {
            _logger.LogInformation(
                "Creating order for ProductId: {ProductId}, Quantity: {Quantity}",
                dto.ProductId,
                dto.Quantity);

            if (dto.Quantity <= 0)
            {
                throw new ArgumentException(
                    "Quantity must be greater than zero.");
            }

            var product = await _productApiClient.GetProductAsync(
                dto.ProductId);

            if (product == null)
            {
                throw new KeyNotFoundException(
                    "Product not found.");
            }

            if (!product.IsActive)
            {
                throw new InvalidOperationException(
                    "Cannot place an order for an inactive product.");
            }

            var stockReduced = await _productApiClient.ReduceStockAsync(
                dto.ProductId,
                dto.Quantity);

            if (!stockReduced)
            {
                throw new InvalidOperationException(
                    "Insufficient stock or stock reduction failed.");
            }

            var order = new Order
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                OrderStatus = "CREATED",
                CreatedAt = DateTime.UtcNow
            };

            var createdOrder = await _orderRepository.CreateAsync(order);

            _logger.LogInformation(
                "Order created successfully. OrderId: {OrderId}",
                createdOrder.OrderId);

            return MapToResponseDto(createdOrder);
        }

        public async Task<OrderResponseDto?> GetByIdAsync(
            Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                return null;
            }

            return MapToResponseDto(order);
        }

        public async Task<PagedOrderResponseDto> GetAllAsync(
            int pageNumber,
            int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            if (pageSize > 100)
            {
                pageSize = 100;
            }

            var orders = await _orderRepository.GetAllAsync(
                pageNumber,
                pageSize);

            var totalItems =
                await _orderRepository.GetTotalCountAsync();

            var totalPages = (int)Math.Ceiling(
                totalItems / (double)pageSize);

            return new PagedOrderResponseDto
            {
                Items = orders
                    .Select(MapToResponseDto)
                    .ToList(),

                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        private static OrderResponseDto MapToResponseDto(
            Order order)
        {
            return new OrderResponseDto
            {
                OrderId = order.OrderId,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                OrderStatus = order.OrderStatus,
                CreatedAt = order.CreatedAt
            };
        }
    }
}
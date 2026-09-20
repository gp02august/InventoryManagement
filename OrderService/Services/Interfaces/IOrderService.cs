using OrderService.DTO;

namespace OrderService.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateAsync(CreateOrderDto dto);

        Task<OrderResponseDto?> GetByIdAsync(Guid orderId);

        Task<PagedOrderResponseDto> GetAllAsync(
            int pageNumber,
            int pageSize);
    }
}
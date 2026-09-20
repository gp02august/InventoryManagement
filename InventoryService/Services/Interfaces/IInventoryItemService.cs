using InventoryService.DTOs;

namespace InventoryService.Services.Interfaces
{
    public interface IInventoryItemService
    {
        Task<InventoryItemResponseDto> CreateAsync(
            CreateInventoryItemDto dto);

        Task<InventoryItemResponseDto?> GetByIdAsync(
            Guid itemId);

        Task<PagedInventoryItemResponseDto> GetAllAsync(
            int pageNumber,
            int pageSize);

        Task<InventoryItemResponseDto?> UpdateAsync(
            Guid itemId,
            UpdateInventoryItemDto dto);

        Task<bool> SoftDeleteAsync(Guid itemId);
    }
}
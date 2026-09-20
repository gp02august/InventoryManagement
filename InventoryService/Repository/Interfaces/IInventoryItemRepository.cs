using InventoryService.Entities;

namespace InventoryService.Repository.Interfaces
{
    public interface IInventoryItemRepository
    {
        Task<InventoryItem> CreateAsync(InventoryItem item);

        Task<InventoryItem?> GetByIdAsync(Guid itemId);

        Task<List<InventoryItem>> GetAllAsync(
            int pageNumber,
            int pageSize);

        Task<int> GetTotalCountAsync();
        Task UpdateAsync(InventoryItem item);

        Task SoftDeleteAsync(InventoryItem item);
    }
}
using InventoryService.Data;
using InventoryService.Entities;
using InventoryService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Repository
{
    public class InventoryItemRepository : IInventoryItemRepository
    {
        private readonly InventoryDbContext _context;

        public InventoryItemRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<InventoryItem> CreateAsync(InventoryItem item)
        {
            await _context.InventoryItems.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<InventoryItem?> GetByIdAsync(Guid itemId)
        {
            return await _context.InventoryItems
                .FirstOrDefaultAsync(x => x.ItemId == itemId);
        }

        public async Task<List<InventoryItem>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.InventoryItems
                .Where(x => x.IsActive)
                .OrderBy(x=>x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> GetTotalCountAsync()
        {
            return await _context.InventoryItems
                .CountAsync(x => x.IsActive);
        }

        public async Task UpdateAsync(InventoryItem item)
        {
            _context.InventoryItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(InventoryItem item)
        {
            item.IsActive = false;
            item.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
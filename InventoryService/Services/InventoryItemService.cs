using InventoryService.DTOs;
using InventoryService.Entities;
using InventoryService.Repository.Interfaces;
using InventoryService.Services.Interfaces;

namespace InventoryService.Services
{
    public class InventoryItemService : IInventoryItemService
    {
        private readonly IInventoryItemRepository _repository;

        public InventoryItemService(
            IInventoryItemRepository repository)
        {
            _repository = repository;
        }
        public async Task<InventoryItemResponseDto> CreateAsync(
            CreateInventoryItemDto dto)
        {
            var item = new InventoryItem
            {
                ItemId = Guid.NewGuid(),
                ItemName = dto.ItemName,
                Category = dto.Category,
                Quantity = dto.Quantity,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var createdItem = await _repository.CreateAsync(item);

            return MapToResponseDto(createdItem);
        }
        public async Task<InventoryItemResponseDto?> GetByIdAsync(
            Guid itemId)
        {
            var item = await _repository.GetByIdAsync(itemId);

            if (item == null || !item.IsActive)
            {
                return null;
            }

            return MapToResponseDto(item);
        }
        public async Task<PagedInventoryItemResponseDto> GetAllAsync(
            int pageNumber,
            int pageSize)
        {
            var totalItems = await _repository.GetTotalCountAsync();

            var items = await _repository.GetAllAsync(
                pageNumber,
                pageSize);

            var totalPages = (int)Math.Ceiling(
                totalItems / (double)pageSize);

            return new PagedInventoryItemResponseDto
            {
                Items = items
                    .Select(MapToResponseDto)
                    .ToList(),

                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }
        public async Task<InventoryItemResponseDto?> UpdateAsync(
            Guid itemId,
            UpdateInventoryItemDto dto)
        {
            var item = await _repository.GetByIdAsync(itemId);

            if (item == null || !item.IsActive)
            {
                return null;
            }

            item.ItemName = dto.ItemName;
            item.Category = dto.Category;
            item.Quantity = dto.Quantity;
            item.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(item);

            return MapToResponseDto(item);
        }
        public async Task<bool> SoftDeleteAsync(Guid itemId)
        {
            var item = await _repository.GetByIdAsync(itemId);

            if (item == null || !item.IsActive)
            {
                return false;
            }

            await _repository.SoftDeleteAsync(item);

            return true;
        }
        private static InventoryItemResponseDto MapToResponseDto(
            InventoryItem item)
        {
            return new InventoryItemResponseDto
            {
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                Category = item.Category,
                Quantity = item.Quantity,
                IsActive = item.IsActive,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            };
        }
    }
}
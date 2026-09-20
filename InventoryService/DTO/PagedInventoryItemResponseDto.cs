using InventoryService.DTOs;

namespace InventoryService.DTOs
{
    public class PagedInventoryItemResponseDto
    {
        public List<InventoryItemResponseDto> Items { get; set; } = new();

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }
    }
}
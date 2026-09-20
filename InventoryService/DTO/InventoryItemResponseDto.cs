namespace InventoryService.DTOs
{
    public class InventoryItemResponseDto
    {
        public Guid ItemId { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public string? Category { get; set; }

        public int Quantity { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
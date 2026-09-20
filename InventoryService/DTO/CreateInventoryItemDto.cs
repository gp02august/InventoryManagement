using System.ComponentModel.DataAnnotations;

namespace InventoryService.DTOs
{
    public class CreateInventoryItemDto
    {
        [Required]
        [MaxLength(150)]
        public string ItemName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Category { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
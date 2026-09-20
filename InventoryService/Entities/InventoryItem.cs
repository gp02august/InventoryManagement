using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Entities
{
    [Table("inventory_items")]
    public class InventoryItem
    {
        [Key]
        [Column("item_id")]
        public Guid ItemId { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("item_name")]
        public string ItemName { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("category")]
        public string? Category { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
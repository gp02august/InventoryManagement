using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Entities
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;

        [Column("price")]
        public decimal Price { get; set; }

        [Column("stock_qty")]
        public int StockQty { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
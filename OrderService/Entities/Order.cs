using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Entities
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [Column("order_id")]
        public Guid OrderId { get; set; }

        [Required]
        [Column("product_id")]
        public Guid ProductId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("order_status")]
        public string OrderStatus { get; set; } = "CREATED";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
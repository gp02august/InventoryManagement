using System.ComponentModel.DataAnnotations;

namespace ProductService.DTO
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQty { get; set; }
    }
}
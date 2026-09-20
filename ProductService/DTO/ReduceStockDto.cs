using System.ComponentModel.DataAnnotations;

namespace ProductService.DTO
{
    public class ReduceStockDto
    {
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
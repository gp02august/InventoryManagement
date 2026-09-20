using ProductService.DTO;

namespace ProductService.DTO
{
    public class PagedProductResponseDto
    {
        public List<ProductResponseDto> Items { get; set; } = new();

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }
    }
}
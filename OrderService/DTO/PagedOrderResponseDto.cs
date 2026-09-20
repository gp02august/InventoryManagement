namespace OrderService.DTO
{
    public class PagedOrderResponseDto
    {
        public List<OrderResponseDto> Items { get; set; } = new();

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }
    }
}
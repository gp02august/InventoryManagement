using Microsoft.AspNetCore.Mvc;
using OrderService.DTO;
using OrderService.Services.Interfaces;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateOrderDto dto)
        {
            var result = await _orderService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { orderId = result.OrderId },
                result);
        }

        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetById(Guid orderId)
        {
            var result = await _orderService.GetByIdAsync(orderId);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Order not found."
                });
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _orderService.GetAllAsync(
                pageNumber,
                pageSize);

            return Ok(result);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using ProductService.DTO;
using ProductService.Services.Interfaces;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateProductDto dto)
        {
            var product = await _productService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { productId = product.ProductId },
                product);
        }

        [HttpGet("{productId:guid}")]
        public async Task<IActionResult> GetById(Guid productId)
        {
            var product = await _productService
                .GetByIdAsync(productId);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Product not found."
                });
            }

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var products = await _productService
                .GetAllAsync(pageNumber, pageSize);

            return Ok(products);
        }

        [HttpPut("{productId:guid}")]
        public async Task<IActionResult> Update(
            Guid productId,
            [FromBody] UpdateProductDto dto)
        {
            var product = await _productService
                .UpdateAsync(productId, dto);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Product not found."
                });
            }

            return Ok(product);
        }

        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> Delete(Guid productId)
        {
            var deleted = await _productService
                .SoftDeleteAsync(productId);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Product not found."
                });
            }

            return Ok(new
            {
                message = "Product deleted successfully."
            });
        }

        [HttpPost("{productId:guid}/reduce-stock")]
        public async Task<IActionResult> ReduceStock(
            Guid productId,
            [FromBody] ReduceStockDto dto)
        {
            var success = await _productService
                .ReduceStockAsync(productId, dto.Quantity);

            if (!success)
            {
                return BadRequest(new
                {
                    message = "Insufficient stock or product not found."
                });
            }

            return Ok(new
            {
                message = "Stock reduced successfully."
            });
        }
    }
}
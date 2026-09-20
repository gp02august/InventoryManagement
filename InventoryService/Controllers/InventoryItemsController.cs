using InventoryService.DTOs;
using InventoryService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/inventory-items")]
    public class InventoryItemsController : ControllerBase
    {
        private readonly IInventoryItemService _service;

        public InventoryItemsController(IInventoryItemService service)
        {
            _service = service;
        }

        // POST: api/inventory-items
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateInventoryItemDto dto)
        {
            var createdItem = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdItem.ItemId },
                createdItem);
        }

        // GET: api/inventory-items/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound(new
                {
                    message = "Inventory item not found."
                });
            }

            return Ok(item);
        }

        // GET: api/inventory-items
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize=10)
        {
            if(pageNumber < 1)
            {
                return BadRequest(new
                {
                    message = "Page number must be greater than 0."
                });
            }
            if (pageSize > 100)
            {
                return BadRequest(new
                {
                    message = "Page size must be between 1 and 100."
                });
            }
            var result = await _service.GetAllAsync(pageNumber, pageSize);

            return Ok(result);
        }
        // PUT: api/inventory-items/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateInventoryItemDto dto)
        {
            var updatedItem = await _service.UpdateAsync(id, dto);

            if (updatedItem == null)
            {
                return NotFound(new
                {
                    message = "Inventory item not found."
                });
            }

            return Ok(updatedItem);
        }

        // DELETE: api/inventory-items/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var deleted = await _service.SoftDeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Inventory item not found."
                });
            }

            return NoContent();
        }
    }
}
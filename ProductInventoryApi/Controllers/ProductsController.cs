using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductInventoryApi.Data;
using ProductInventoryApi.DTOs;
using ProductInventoryApi.Middleware;
using ProductInventoryApi.Models;
using ProductInventoryApi.Models.DTOs;
namespace ProductInventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(
            string? search,
            string? sortBy,
            bool sortDesc = false,
            int pageNumber = 1,
            int pageSize = 10)

        {
            var products = await _service.GetAllAsync(search, sortBy, sortDesc, pageNumber, pageSize);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(CreateProductDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var product = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var productUpdated = await _service.UpdateAsync(id, dto);
            if (!productUpdated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productDeleted = await _service.DeleteAsync(id);
            if (!productDeleted) return NotFound();

            return NoContent();
        }
    }
}

using DPWDR.Technical.Interview.Data.Entities;
using DPWDR.Technical.Interview.Services.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DPWDR.Technical.Interview.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
                [FromQuery] DateTime? startDate,
                [FromQuery] int? productId)
        {
            try
            {
                var products = await _productService.GetProductsInStockAsync(startDate, productId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }

        }

        [HttpPut]
        [Route("api/products/{id}")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] int newStock)
        {
            var existingProduct = await _productService.ProductExists(id);

            if (!existingProduct)
            {
                return BadRequest("Producto no encontrado.");

            }

            var result = await _productService.UpdateStockIfZero(id, newStock);
            return Ok(result);

        }
    }
}

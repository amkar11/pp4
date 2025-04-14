using Microsoft.AspNetCore.Mvc;
using EShop.Application;
using EShop.Domain;
using EShop.Domain.ProductProvidersExceptions;
using System.Net;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductService _productService;
        public ProductController (IProductService productService) 
        {
            _productService = productService;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult ShowAllProducts()
        {
            var products = _productService.ShowAllProducts();
            return Ok(products);
        }
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            return Ok(product);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            try
            {
                _productService.AddProduct(product);
                return Created($"api/products/{product.Id}", product);
            }
            catch (ProductAllreadyExistsException ex)
            {
                return BadRequest(new { error = $"{ex.Message}", code = HttpStatusCode.BadRequest } );
            }

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult ChangeProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id) return BadRequest("Id you entered and actual product Id does not match!");
            _productService.UpdateProduct(product);
            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (ProductDoesNotExistException ex) 
            {
                return BadRequest(new { error = $"{ex.Message}", code = HttpStatusCode.BadRequest });
            }
        }
    }
}

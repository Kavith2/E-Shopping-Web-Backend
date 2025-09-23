using Backend1.Data;
using Backend1.IService;
using Backend1.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Backend1.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return _productService.GetAllProducts();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound();

            return product;
        }

        [HttpGet("category/{category}")]
        public IActionResult GetByCategory(string category)
        {
            var products = _productService.GetProductsByCategory(category);
            return Ok(products);
        }


        [HttpPost]
        public ActionResult AddProduct([FromBody] List<Product> product)
        {
            _productService.AddProduct(product);
            return Ok(new { message = "Product added successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Product updatedProduct)
        {
            var result = await _productService.UpdateProduct(id, updatedProduct);

            if (result == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(result);
        }
    }

}

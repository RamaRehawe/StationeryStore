using Microsoft.AspNetCore.Mvc;
using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly DataContext _context;
        public ProductController(IProductRepository productRepository, DataContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(products);
        }
    }
}

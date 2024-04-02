using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StationeryStore.Data;
using StationeryStore.Dto;
using StationeryStore.Interfaces;
using StationeryStore.Models;
using StationeryStore.Repository;

namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        [HttpGet("productId")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
                return NotFound();
            var product = _mapper.Map<ProductDto>(_productRepository.GetProduct(productId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(product);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddProduct([FromBody] ProductDto productAdd)
        {
            if (productAdd == null)
                return BadRequest(ModelState);

            var product = _productRepository.GetProducts()
                .Where(p => p.Name.Trim().ToUpper() == productAdd.Name.TrimEnd().ToUpper())
                .Where(p => p.Description.Trim().ToUpper() == productAdd.Description.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (product != null)
            {
                ModelState.AddModelError("", "Product already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productAdd);
            if (!_productRepository.AddProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully Added");
        }
    }
}

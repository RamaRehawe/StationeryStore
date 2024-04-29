using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("subId")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts(int subId)
        {
            var products = _mapper.Map<List<ResProductDto>>(_productRepository.GetProducts(subId));

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
            var product = _mapper.Map<ResProductDto>(_productRepository.GetProduct(productId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(product);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddProduct([FromBody] ReqProductDto productAdd)
        {
            if (productAdd == null)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productAdd);
            var product = _productRepository.GetProducts(productMap.SubCategoryId)
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

            if (!_productRepository.AddProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully Added");
        }

        [HttpGet]
        public IActionResult GetAllProducts(string? search)
        {
            string s;
            if (!string.IsNullOrEmpty(search))
                s = search;
            else s = "";
            var products = _mapper.Map<List<ResProductDto>>(_productRepository.GetAllProducts(s));
            return Ok(products);
        }

    
    }
}
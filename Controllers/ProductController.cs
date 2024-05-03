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
            var pro = _productRepository.GetProduct(productId);
            var product = _mapper.Map<ResProductDto>(pro);
            var list1 = product.ProductAttributeQuantities.ToList();
            var list2 = pro.ProductAttributeQuantities.ToList();
            for (int i = 0; i < list1.Count; i++)
            {
                if (list2[i].ProductAttributes.Count > 0)
                {
                    list1[i].Name1 = list2[i].ProductAttributes.ToList()[0].Attribute.Name;
                    list1[i].Value1 = list2[i].ProductAttributes.ToList()[0].Value;
                }
                if (list2[i].ProductAttributes.Count > 1)
                {
                    list1[i].Name2 = list2[i].ProductAttributes.ToList()[1].Attribute.Name;
                    list1[i].Value2 = list2[i].ProductAttributes.ToList()[1].Value;
                }
                list1[i].ProductAttributeQuantityId = list2[i].Id;

            }
            product.ProductAttributeQuantities = list1;
            int x = 1;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(product);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        //[Authorize(Roles = "Item Manager")]
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
            return Ok(new { message = " Added successfully!" });
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
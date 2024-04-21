﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StationeryStore.Dto;
using StationeryStore.Interfaces;
using StationeryStore.Models;
using StationeryStore.Services;

namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = "Item Manager")]
    public class AttributeController : BaseController
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IMapper _mapper;
        public AttributeController(IAttributeRepository attributeRepository, IProductAttributeRepository productAttributeRepository,
            IMapper mapper, UserInfoService userInfoService, 
            IUserRepository userRepository) : base(userInfoService, userRepository)
        {
            _attributeRepository = attributeRepository;
            _productAttributeRepository = productAttributeRepository;
            _mapper = mapper;
        }



        // GET api/product/{productId}/attributes
        [HttpGet("attributes")]
        public IActionResult GetAttributeAndProductAttributes(int productId)
        {
            var attributes = _mapper.Map<List<ResAttributeDto>>(_attributeRepository.GetAttributesForProduct(productId));
            var productAttributes = _mapper.Map<List<ResProductAttributeDto>>(_productAttributeRepository.GetProductAttributes(productId));

            var result = new
            {
                Atributes = attributes,
                ProductAttributes = productAttributes
            };

            return Ok(result);
        }


        // POST api/product/{productId}/attributes
        [HttpPost("{productId}/attributes")]
        public IActionResult AddProductAttributes(int productId, [FromBody] ReqAttributeDto attributeDto)
        {
            var attribute = new Atribute
            {
                Name = attributeDto.Name,
            };
            if(_attributeRepository.Exist(attribute.Name))
            {
                return BadRequest("The Attribute Already exist");
            }
            var attributeId = _attributeRepository.AddAttribute(attribute);

            var attributeProduct = new ProductAttribute
            {
                AttributeId = attributeId,
                ProductAttributeQuantityId = attributeDto.ProductAttributeQuantityId,
                Value = attributeDto.Value
                
            };
            if(_productAttributeRepository.Exist(attributeProduct.Value))
            {
                return BadRequest("The value already exist");
            }
            _productAttributeRepository.AddProductAttribute(attributeProduct);

            return Ok("Attributes and product attributes added successfully.");
        }


        [HttpPost("createAttribute")]
        public IActionResult CreateAttribute(ReqAttributeDto attribute)
        {
            var attributeMap = _mapper.Map<Atribute>(attribute);
            _attributeRepository.AddAttribute(attributeMap);
            return Ok("Attribute Added Successfully");
        }

        [HttpGet("getAllAttribute")]
        public IActionResult GetAttributes() 
        {
            var attributes = _mapper.Map<List<ResAttributeDto>>(_attributeRepository.GetAttributes());
            return Ok(attributes);
        }

        [HttpGet("selectAttribute")]
        public IActionResult SelectAttribute(int id) 
        {
            var attribute = _mapper.Map<ResAttributeDto>(_attributeRepository.GetAttribute(id));
            if(attribute == null)
                return NotFound();
            return Ok(attribute);
        }
    }
}

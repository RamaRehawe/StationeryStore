using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StationeryStore.Dto;
using StationeryStore.Interfaces;
using StationeryStore.Models;
using StationeryStore.Services;
using System;

namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize (Roles = "Item Manager")]
    public class AttributeController : BaseController
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IMapper _mapper;
        private readonly IImageAttributeRepository _imageAttributeRepository;
        private readonly IProductAttributeQuantityRepository _productAttributeQuantityRepository;

        public AttributeController(IAttributeRepository attributeRepository, 
            IProductAttributeRepository productAttributeRepository, IMapper mapper,
            IProductAttributeQuantityRepository productAttributeQuantityRepository,
            IImageAttributeRepository imageAttributeRepository, UserInfoService userInfoService, 
            IUserRepository userRepository) : base(userInfoService, userRepository)
        {
            _attributeRepository = attributeRepository;
            _productAttributeRepository = productAttributeRepository;
            _mapper = mapper;
            _imageAttributeRepository = imageAttributeRepository;
            _productAttributeQuantityRepository = productAttributeQuantityRepository;
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
        [HttpPost("addDetails")]
        public  async Task<IActionResult> AddProductAttributes(IFormFile formFile,
            [FromForm] ReqAttributeDto attributeDto)
        {
            var productAttributeQuantity = new ProductAttributeQuantity
            {
                Quantity = attributeDto.Quantity,
                Price = attributeDto.Price,
                ProductId = attributeDto.ProductId
            };
            int productAttributeQuantityId = _productAttributeQuantityRepository.Create(productAttributeQuantity);


            var attribute = new Atribute
            {
                Name = attributeDto.Name
            };
            var attributeId = 0;
            if (_attributeRepository.Exist(attribute.Name))
            {
                attributeId = _attributeRepository.GetAttributeId(attribute.Name);
            }
            else 
                attributeId = _attributeRepository.AddAttribute(attribute);

            var attributeProduct = new ProductAttribute
            {
                AttributeId = attributeId,
                ProductAttributeQuantityId = productAttributeQuantityId,
                Value = attributeDto.Value
                
            };

            if(!_productAttributeRepository.Exist(attributeProduct.Value, attributeId))
            {
                _productAttributeRepository.AddProductAttribute(attributeProduct);
            }

            var res1 = WriteFile(formFile);
            var res = await res1;
            if(res.Equals(Empty))
            {
                return BadRequest("file not valid");
            }
            var imageAttribute = new ImageAttribute
            {
                URL = res,
                ProductAttributeQuantityId = productAttributeQuantityId
            };
            _imageAttributeRepository.AddImage(imageAttribute);
            
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


        private async Task<string> WriteFile(IFormFile file)
        {
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                filename = base.GetActiveUser()!.Username + DateTime.Now.ToString("MMddyyyyHHmm") + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Product");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Product", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return Path.Combine("wwwroot/Upload/Product", filename); ;
            }
            catch (Exception ex)
            {
                return "";
            }
            
        }
    }
}

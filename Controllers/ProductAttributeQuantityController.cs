using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StationeryStore.Interfaces;
using StationeryStore.Models;
using StationeryStore.Services;
using StationeryStore.Dto;
using Microsoft.AspNetCore.Authorization;


namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Item Manager")]
    public class ProductAttributeQuantityController : BaseController
    {
        private readonly IProductAttributeQuantityRepository _productAttributeQuantityRepository;
        private readonly IMapper _mapper;
        public ProductAttributeQuantityController(IProductAttributeQuantityRepository productAttributeQuantityRepository, IMapper mapper,
            UserInfoService userInfoService, IUserRepository userRepository) : base(userInfoService, userRepository)
        {
            _productAttributeQuantityRepository = productAttributeQuantityRepository;
            _mapper = mapper;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateQuantity(int id, ReqProductAttributeQuantityDto quantityDto)
        {
            var isExisted = _productAttributeQuantityRepository.GetById(id);
            if(isExisted == null)
            {
                return NotFound("Product Not Found");
            }

            var updatedQuantity = _mapper.Map(quantityDto, isExisted);
            if (!_productAttributeQuantityRepository.Update(updatedQuantity))
                return BadRequest("Somthing went wrong");
            return Ok(new { message = "Updated successfully" });
        }
    }
}

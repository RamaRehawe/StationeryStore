using AutoMapper;
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
    public class AddressController : BaseController
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressController(IAddressRepository addressRepository, IMapper mapper, 
            UserInfoService userInfoService, IUserRepository userRepository)
            : base(userInfoService, userRepository)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
        [Authorize (Roles = "Customer")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Address))]
        [ProducesResponseType(400)]
        public IActionResult GetMyAddresses()
        {
            var userId = base.GetActiveUser()!.Id;
            var addresses = _mapper.Map<List<AddressDto>>(_addressRepository.GetAddressByUser(userId));
            if(addresses == null)
                return NotFound();
            return Ok(addresses);
        }

        [Authorize (Roles = "Customer")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddAddress([FromBody] AddressDto addressAdd)
        {
            if(addressAdd == null)
                return BadRequest(ModelState);

            var addressMap = _mapper.Map<Address>(addressAdd);
            addressMap.UserId = base.GetActiveUser()!.Id;

            var isExisted = _addressRepository.GetAddresses()
                .Where(a => a.UserId ==  addressMap.UserId)
                .Where(a => a.Title.Trim().ToUpper() == addressMap.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(isExisted != null)
            {
                ModelState.AddModelError("", "Address already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_addressRepository.AddAddress(addressMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully Added");
        }
    }
}

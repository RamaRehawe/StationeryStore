using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StationeryStore.Dto;
using StationeryStore.Interfaces;
using StationeryStore.Models;

namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public AddressController(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Address>))]
        public IActionResult GetAddresses()
        {
            var address = _mapper.Map<List<AddressDto>>(_addressRepository.GetAddresses());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(address);
        }

        [HttpGet("{addressId}")]
        [ProducesResponseType(200, Type = typeof(Address))]
        [ProducesResponseType(400)]

        public IActionResult GetAdress(int addressId)
        {
            if (!_addressRepository.AddressExists(addressId))
                return NotFound();

            var address = _mapper.Map<AddressDto>(_addressRepository.GetAddress(addressId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(address);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddAddress([FromBody] AddressDto addressAdd)
        {
            if(addressAdd == null)
                return BadRequest(ModelState);

            var address = _addressRepository.GetAddresses()
                .Where(a => a.Title.Trim().ToUpper() == addressAdd.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(address != null)
            {
                ModelState.AddModelError("", "Address already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addressMap = _mapper.Map<Address>(addressAdd);
            if(!_addressRepository.AddAddress(addressMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully Added");
        }
    }
}

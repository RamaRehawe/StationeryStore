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
    public class CustomerServiceController : BaseController
    {
        private readonly ICustomerServiceRepository _customerServiceRepository;
        private readonly IMapper _mapper;
        public CustomerServiceController(ICustomerServiceRepository customerServiceRepository,
            IMapper mapper,
            UserInfoService userInfoService, IUserRepository userRepository) : base(userInfoService, userRepository)
        {
            _customerServiceRepository = customerServiceRepository;
            _mapper = mapper;
        }

        [HttpPost("addComplain")]
        //[Authorize (Roles = "Customer")]
        public IActionResult AddComplain(ReqCustomerServiceDto complain)
        {
            var user = base.GetActiveUser()!;
            var customerService = new CustomerService
            {
                Type = complain.Type,
                Details = complain.Details,
                UserId = user.Id,
                AdminResponse = false
            };
            _customerServiceRepository.AddComplain(customerService);
            return Ok(complain);
        }

        //[Authorize]
        [HttpGet("getComplains")]
        public IActionResult GetComplains()
        {
            var complains = _mapper.Map<List<ResCustomerServiceDto>>(_customerServiceRepository.GetComplains());
            return Ok(complains);
        }

        //[Authorize (Roles = "Customer")]
        [HttpGet("getMyComplains")]
        public IActionResult GetMyComplains()
        {
            var userId = base.GetActiveUser()!.Id;
            var complains = _mapper.Map<List<ResCustomerServiceDto>>( _customerServiceRepository.GetMyComplainsByUserId(userId));
            return Ok(complains);
        }

        //[Authorize (Roles = "Admin")]
        [HttpPost("adminResponse")]
        public IActionResult SetResponse(int complaineId)
        {
            _customerServiceRepository.SetResponse(complaineId);
            return Ok("Done");
        }
    }
}

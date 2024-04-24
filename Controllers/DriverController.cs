using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StationeryStore.Dto;
using StationeryStore.Interfaces;
using StationeryStore.Models;
using StationeryStore.Services;
using System.Collections.Generic;

namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize (Roles = "Driver")]
    public class DriverController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepository;
        public DriverController(IMapper mapper, IDriverRepository driverRepository,
            UserInfoService userInfoService, IUserRepository userRepository) : base(userInfoService, userRepository)
        {
            _mapper = mapper;
            _driverRepository = driverRepository;
        }

        [HttpPost("setDriverStatus")]
        public IActionResult SetDriverStatus(ReqDriverDto driverDto)
        {
            var driverMap = _mapper.Map<Driver>(driverDto);
            var userId = base.GetActiveUser()!.Id;
            var driver = _driverRepository.GetDrivers().Where(d => d.UserId == userId).FirstOrDefault()!;
            _driverRepository.SetDriverStatus(driver.Id, driver.DriverStatus);
            return Ok(driver.DriverStatus);
        }

        [HttpGet("pendingOrders")]
        public IActionResult GetPendingOrders()
        {
            var pendingOrderDto = new List<PendingOrderDto>();
            var pendingOrders = _driverRepository.GetPendingOrders();
            foreach(var pendingOrder in pendingOrders)
            {
                var OrderDto = new PendingOrderDto
                {
                    OrderStatus = pendingOrder.OrderStatus,
                    Username = pendingOrder.User.Username,
                    Phone = pendingOrder.User.Phone,
                    Title = pendingOrder.Address.Title

                };
                pendingOrderDto.Add(OrderDto);
            }
            //var pendingOrders = _mapper.Map<List<PendingOrderDto>>(_driverRepository.GetPendingOrders());
            return Ok(pendingOrderDto);

        }

        [HttpPost("selectOrder")]
        public IActionResult SelectOrder(int orderId)
        {
            var userId = base.GetActiveUser()!.Id;
            var driver = _driverRepository.GetDrivers().Where(d => d.UserId == userId).FirstOrDefault();
            _driverRepository.SelectOrder(orderId, driver.Id);
            return Ok("Order Selected Successfully");
        }

        [HttpPost("shipped")]
        public IActionResult UpdateOrderStatueToShipped(int orderId)
        {
            var userId = base.GetActiveUser()!.Id;
            var driver = _driverRepository.GetDrivers().Where(d => d.UserId == userId).FirstOrDefault();
            _driverRepository.UpdateOrderStatueToShipped(orderId);
            return Ok("The order Has been deliverd");
        }

        [HttpPost("deliverd")]
        public IActionResult UpdateOrderStatueToDeliverd(int orderId)
        {
            var userId = base.GetActiveUser()!.Id;
            var driver = _driverRepository.GetDrivers().Where(d => d.UserId == userId).FirstOrDefault();
            _driverRepository.UpdateOrderStatueToDeliverd(orderId);
            return Ok("The order Has been deliverd");
        }

        [HttpGet("GetMyOrders")]
        public IActionResult GetMyOrders()
        {
            var userId = base.GetActiveUser()!.Id;
            var driver = _driverRepository.GetDrivers().Where(d => d.UserId == userId).FirstOrDefault();
            var orders = _driverRepository.GetMyOrders(driver.Id);
            return Ok(orders);
        }

        [HttpPost("checkDeliverd")]
        public IActionResult CheckDeliverd(CheckOrderDto checkDto)
        {
            var userId = base.GetActiveUser()!?.Id;
            var driver = _driverRepository.GetDrivers().Where(d => d.UserId == userId).FirstOrDefault();
            if (!checkDto.Checked)
            {
                _driverRepository.FailDeliver(checkDto.OrderId, checkDto.FailDeliver);
                return Ok(checkDto.FailDeliver);
            }
            else
            {
                _driverRepository.UpdateOrderStatueToDeliverd(checkDto.OrderId);
                return Ok("Order Has been deliverd");
            }

        }


        [HttpPost("add_driver")]
        public IActionResult AddDriver([FromBody] AddDriverDto driverDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Manually map AddDriverDto properties to Driver entity
                var driver = new Driver
                {
                    // Map properties from AddDriverDto to Driver
                    // Assuming the property names in AddDriverDto match the properties in Driver
                    // If not, adjust accordingly
                    UserId = driverDto.UserId,
                    //DriverLicense = driverDto.DriverLicense,
                    // Add any other properties here
                };

                // Add the driver to the repository
                _driverRepository.AddDriver(driver);

                // Alternatively, you might want to return the created driver's information
                return Ok(driver);
            }
            catch (Exception ex)
            {
                // Handle any exceptions or errors that occur during the operation
                return StatusCode(500, $"An error occurred while adding the driver: {ex.Message}");
            }

        }


    }
}

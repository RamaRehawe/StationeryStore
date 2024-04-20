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
    [Authorize (Roles = "Driver")]
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

        [HttpPost("updateOrderStatue")]
        public IActionResult UpdateOrderStatue(int orderId)
        {
            var userId = base.GetActiveUser()!.Id;
            var driver = _driverRepository.GetDrivers().Where(d => d.UserId == userId).FirstOrDefault();
            _driverRepository.UpdateOrderStatue(orderId);
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

    }
}

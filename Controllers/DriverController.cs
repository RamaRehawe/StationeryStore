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
                    Id = pendingOrder.Id,
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
            var driver = _driverRepository.GetDrivers().Where(d => d.UserId == userId).First();
            if (_driverRepository.SelectOrder(orderId, driver.Id))
                return Ok("Order Selected Successfully");
            else
                return BadRequest("order cannot be selected");
        }

        [HttpPost("loading")]
        public IActionResult UpdateOrderStatueToLoading(int orderId)
        {
            var userId = base.GetActiveUser()!.Id;
            var driver = _driverRepository.GetDrivers().Where(d => d.UserId == userId).FirstOrDefault();
            _driverRepository.UpdateOrderStatueToShipped(orderId);
            return Ok("The order Has been loading");
        }

        [HttpPost("shipped")]
        public IActionResult UpdateOrderStatueToShipped(int orderId)
        {
            var userId = base.GetActiveUser()!.Id;
            var driver = _driverRepository.GetDrivers().Where(d => d.UserId == userId).FirstOrDefault();
            _driverRepository.UpdateOrderStatueToShipped(orderId);
            return Ok("The order Has been shipped");
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
            var driver = _driverRepository.GetDrivers().Where(d => d.UserId == userId).First();
            var ord = _driverRepository.GetMyOrders(driver.Id);
            var orders = _mapper.Map<List<ResGetMyOrdersDto>>(ord);
            for(int i=0; i<orders.Count; i++)
            {
                var list = orders[i].OrderItems.ToList();
                var list2 = ord[i].OrderItems.ToList();
                
                for (int j = 0; j < orders[i].OrderItems.Count; j++)
                {
                    list[j].Name = list2[j].ProductAttributeQuantity.Product.Name;
                    if(list2[j].ProductAttributeQuantity.ProductAttributes.Count > 0)
                    {
                        list[j].Atribute = list2[j].ProductAttributeQuantity.ProductAttributes.ToList()[0].Attribute.Name;
                        list[j].Value = list2[j].ProductAttributeQuantity.ProductAttributes.ToList()[0].Value;

                    }
                    if(list2[j].ProductAttributeQuantity.ProductAttributes.Count > 1)
                    {
                        list[j].Atribute2 = list2[j].ProductAttributeQuantity.ProductAttributes.ToList()[1].Attribute.Name;
                        list[j].Value2 = list2[j].ProductAttributeQuantity.ProductAttributes.ToList()[1].Value;

                    }
                    
                }
                orders[i].OrderItems = list;
            }

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


    }
}

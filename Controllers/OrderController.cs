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
    public class OrderController : BaseController
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        public OrderController(IOrderRepository orderRepository, ICartRepository cartRepository, IMapper mapper,
            UserInfoService userInfoService, IUserRepository userRepository) : base(userInfoService, userRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        [HttpGet ("orders")]
        //[Authorize (Roles = "Customer")]
        public IActionResult GetMyOrders()
        {
            var userId = base.GetActiveUser()!.Id;
            var orders = _orderRepository.GetOrdersByUserId(userId);
            var ordersDto = _mapper.Map<List<ResOrderDto>>(orders);
            return Ok(ordersDto);
        }

        [HttpGet("deliverdOrders")]
        //[Authorize(Roles = "Customer")]
        public IActionResult GetMyDeliverdOrders()
        {
            var userId = base.GetActiveUser()!.Id;
            var orders = _orderRepository.GetDeliverdOrdersByUserId(userId);
            var ordersDto = _mapper.Map<List<ResOrderDto>>(orders);
            return Ok(ordersDto);
        }

        [HttpGet("notDeliverdOrders")]
        //[Authorize(Roles = "Customer")]
        public IActionResult GetMyNotDeliverdOrders()
        {
            var userId = base.GetActiveUser()!.Id;
            var orders = _orderRepository.GetNotDeliverdOrdersByUserId(userId);
            var ordersDto = _mapper.Map<List<ResOrderDto>>(orders);
            return Ok(ordersDto);
        }


        [HttpGet ("{orderId}")]
        //[Authorize (Roles = "Customer")]
        public IActionResult GetOrderById(int orderId)
        {
            var userId = base.GetActiveUser()!.Id;
            var order = _orderRepository.GetOrderByOrderId(userId, orderId);
            if(order == null)
            {
                return NotFound("Order Not Found");
            }
            var orderDto = _mapper.Map<ResOrderDto>(order);
            var listDb = order.OrderItems.ToList();
            var list = orderDto.OrderItems.ToList();
            for(int i=0; i< orderDto.OrderItems.Count; i++)
            {
                list[i].Name = listDb[i].ProductAttributeQuantity.Product.Name;
                if (listDb[i].ProductAttributeQuantity.ProductAttributes.Count > 0)
                {
                    list[i].Atribute = listDb[i].ProductAttributeQuantity.ProductAttributes.ToList()[0].Attribute.Name;
                    list[i].Value = listDb[i].ProductAttributeQuantity.ProductAttributes.ToList()[0].Value;
                }
                if (listDb[i].ProductAttributeQuantity.ProductAttributes.Count > 1)
                {
                    list[i].Atribute = listDb[i].ProductAttributeQuantity.ProductAttributes.ToList()[1].Attribute.Name;
                    list[i].Value = listDb[i].ProductAttributeQuantity.ProductAttributes.ToList()[1].Value;
                }
                list[i].Quantity = listDb[i].ProductAttributeQuantity.Quantity;
                list[i].Price = listDb[i].ProductAttributeQuantity.Price;
                list[i].ProductAttributeQuantityId = listDb[i].ProductAttributeQuantityId;

            }
            orderDto.OrderItems = list;
            return Ok(orderDto);
        }

        [HttpPost("placeOrder")]
        //[Authorize (Roles = "Customer")]
        public IActionResult PlaceOrder(PlaceOrderDto placeOrderDto)
        {
            var userId = base.GetActiveUser()!.Id;
            var cart = _cartRepository.GetCartByUserId(userId);
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now.Date,
                OrderStatus = "loading",
                 ShippingCost = 15000,
                AddressId = placeOrderDto.AddressId,
            };
            var cartItems = _cartRepository.GetCartItemsByCartId(cart.Id);
            if (cartItems == null || !cartItems.Any())
            {
                return NotFound("Cart items not found");
            }

            // Check if the quantity for each product in the cart is available
            foreach (var cartItem in cartItems)
            {
                var productAttributeQuantity = _cartRepository.GetProductAttributeQuantityById(cartItem.ProductAttributeQuantityId);
                // Check if the quantity in the cart exceeds the available quantity
                if (cartItem.Quantity > productAttributeQuantity.Quantity)
                {
                    return BadRequest($"Quantity exceeds available stock for product: {productAttributeQuantity.Product.Name}");
                }
            }


            // Add order items to the order
            int orderId = _orderRepository.CreateOrder(order);
            foreach (var cartItem in cartItems)
            {
                var productAttributeQuantity = _cartRepository.GetProductAttributeQuantityById(cartItem.ProductAttributeQuantityId);
                
                // Create a new OrderItem and map properties from CartItem
                var orderItem = new OrderItem
                {
                    Quantity = cartItem.Quantity,
                    Price = productAttributeQuantity.Price,
                    ProductAttributeQuantityId = cartItem.ProductAttributeQuantityId,
                    OrderId = orderId
                };
                productAttributeQuantity.Quantity -= cartItem.Quantity;
                // Add the OrderItem to the Order
                _orderRepository.AddItemToOrder(orderItem);
            }
            // Calculate and set total amount for the order
            order.TotalAmount = CalculateTotalAmount(order.OrderItems) + order.ShippingCost;
            // Clear the user's cart
            _cartRepository.ClearCart(userId);
            return Ok("Order placed successfully");
        }

        [HttpGet ("status")]
        //[Authorize (Roles = "Customer")]
        public IActionResult GetOrderStatus (int orderId)
        {
            var userId = base.GetActiveUser()!.Id;
            var order = _orderRepository.GetOrderByOrderId (userId, orderId);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            var orderStatus = order.OrderStatus;

            if (orderStatus == "Delivered")
            {
                return Ok("This order has been delivered and can no longer be tracked.");
            }

            return Ok(new { Status = orderStatus });
        }

        [HttpGet ("getAllOrders")]
        //[Authorize]
        public IActionResult GetAllOrders()
        {
            var orders = _mapper.Map<List<ResOrderDto>>(_orderRepository.GetOrders());
            return Ok(orders);
        }

        [HttpGet("getOrdersPercentage")]
        public IActionResult GetOrdersPercentage()
        {
            double percentage = CalculateOrderPercentage();
            return Ok(percentage + "%");
        }


        private double CalculateOrderPercentage()
        {
            int totalOrders = _orderRepository.GetTotalOrdersCount();
            int completedOrders = _orderRepository.GetCompletedOrdersCount();

            if (totalOrders == 0)
            {
                return 0; // Avoid division by zero
            }

            return (double)completedOrders / totalOrders * 100;
        }

        private double CalculateTotalAmount(ICollection<OrderItem> orderItems)
        {
            double totalAmount = 0;
            foreach (var orderItem in orderItems)
            {
                totalAmount += orderItem.Price * orderItem.Quantity;
            }
            return totalAmount;
        }

        
    }
}

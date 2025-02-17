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
    public class CartController : BaseController
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        public CartController(ICartRepository cartRepository, IMapper mapper,
            UserInfoService userInfoService, IUserRepository userRepository) : base(userInfoService, userRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }


        [HttpGet("myCart")]
        [Authorize]
        public IActionResult GetCartByUserId()
        {
            var userId = base.GetActiveUser()!.Id;
            var cart = _cartRepository.GetCartByUserId(userId);
            if (cart == null)
            {
                return NotFound("Cart not found");
            }
            var cartItems = _cartRepository.GetCartItemsByCartId(cart.Id);
            // Calculate the subprice for each item and sum up the total price
            double total = 0;
            var cartItemDtos = new List<ResCartItemDto>();
            foreach (var item in cartItems)
            {
                var productAttributeQuantity = _cartRepository.GetProductAttributeQuantityById(item.ProductAttributeQuantityId);

                var subPrice = item.Quantity * productAttributeQuantity.Price;
                total += subPrice;
                // Manually map CartItem to CartItemDto
                var cartItemDto = new ResCartItemDto
                {
                    Quantity = item.Quantity,
                    Price = productAttributeQuantity.Price,
                    productName = productAttributeQuantity.Product.Name,
                    itemId = item.ProductAttributeQuantityId,
                    Subtotal = subPrice
                };
                cartItemDtos.Add(cartItemDto);
            }

            // Create ResCartDto with CartItems and TotalPrice
            var resCartDto = new ResCartDto
            {
                CartItems = cartItemDtos,
                TotalPrice = total
            };

            return Ok(resCartDto);
        }

        //[Authorize(Roles = "Customer")]
        [HttpPost("addItem")]
        public IActionResult AddItemToCart(CartItemDto itemDto)
        {
            var productAttributeQuantity = _cartRepository.GetProductAttributeQuantityById(itemDto.ProductAttributeQuantityId);
            // Check if the product attribute quantity exists
            if (productAttributeQuantity == null)
            {
                return NotFound("Product attribute quantity not found");
            }
            if(productAttributeQuantity.Quantity < itemDto.Quantity)
            {
                return BadRequest($"Quantity exceeds available stock for product: {productAttributeQuantity.Product.Name}");

            }
            var subPrice = itemDto.Quantity * productAttributeQuantity.Price;
            itemDto.SubPrice = subPrice;


            // Get the cart by user ID
            var userId = base.GetActiveUser()!.Id;
            var cart = _cartRepository.GetCartByUserId(userId);

            // Create a new cart if it doesn't exist
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _cartRepository.CreateCart(cart);
            }

            // Product attribute quantity exists, check if the item already exists in the cart
            var existingItem = _cartRepository.GetCartItemByCartAndProductAttributeQuantity(cart.Id, productAttributeQuantity.Id);
            if (existingItem != null)
            {
                if (productAttributeQuantity.Quantity < (itemDto.Quantity + existingItem.Quantity))
                {
                    return BadRequest($"Quantity exceeds available stock for product: {productAttributeQuantity.Product.Name}");

                }
                existingItem.Quantity += itemDto.Quantity;
                _cartRepository.UpdateCartItem(existingItem);
                return Ok("Item quantity updated successfully");
            }
            else
            {
                // Create a new item and add it to the cart
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductAttributeQuantityId = itemDto.ProductAttributeQuantityId,
                    Quantity = itemDto.Quantity,
                };
                _cartRepository.AddItemToCart(newItem);
                return Ok("Item added to cart successfully");
            }
        }


        //[Authorize(Roles = "Customer")]
        [HttpDelete("deleteItem/{itemId}")]
        public IActionResult DeleteItemFromCart(int itemId)
        {
            var userId = base.GetActiveUser()!.Id;
            var cart = _cartRepository.GetCartByUserId(userId);

            // Check if the cart exists
            if (cart == null)
            {
                return NotFound("Cart not found");
            }

            // Retrieve the item from the cart
            var existingItem = _cartRepository.GetCartItemById(itemId, cart.Id);

            // Check if the item exists in the cart
            if (existingItem == null)
            {
                return NotFound("Item not found in the cart");
            }

            // Delete the item from the cart
            _cartRepository.DeleteCartItem(existingItem);

            return Ok("Item deleted from cart successfully");
        }


    }
}

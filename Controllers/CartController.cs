using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StationeryStore.Interfaces;
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
        // creat the cart
        //[HttpPost]
        //public IActionResult CreatCart()
        //{

        //}


    }
}

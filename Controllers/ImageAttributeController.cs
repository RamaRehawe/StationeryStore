using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StationeryStore.Dto;
using StationeryStore.Interfaces;
using StationeryStore.Services;

namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageAttributeController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IImageAttributeRepository _imageAttributeRepository;
        public ImageAttributeController(IImageAttributeRepository imageAttributeRepository,
            IMapper mapper,
            UserInfoService userInfoService, IUserRepository userRepository) : base(userInfoService, userRepository)
        {
            _mapper = mapper;
            _imageAttributeRepository = imageAttributeRepository;
        }

        [HttpGet("getImage")]
        public IActionResult GetImage(int producAttributeQuantitytId) 
        {
            var images = "https://localhost:7214/";
            images +=   _imageAttributeRepository.GetImages(producAttributeQuantitytId);
            return Ok(images);
        }

       
    }
}

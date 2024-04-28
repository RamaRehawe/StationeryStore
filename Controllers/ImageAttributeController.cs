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
        public ImageAttributeController(UserInfoService userInfoService, IUserRepository userRepository) : base(userInfoService, userRepository)
        {
        }

        [HttpGet("getImage")]
        public IActionResult GetImage(int productId) 
        {
            var images = _mapper.Map<List<ResImageAttributeDto>>( _imageAttributeRepository.GetImages(productId));
            return Ok(images);
        }

        //[HttpPost]
        //public async Task<IActionResult> UploadFile(IFormFile file)
        //{
        //    var result = await WriteFile(file);
        //    return Ok(result);
        //}

        //private async Task<string> WriteFile(IFormFile file)
        //{
        //    string filename = "";
        //    try
        //    {
        //        var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
        //        filename = base.GetActiveUser()!.Username + DateTime.Now.ToString("MMddyyyyHHmm")+ extension;

        //        var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Product");

        //        if (!Directory.Exists(filepath))
        //        {
        //            Directory.CreateDirectory(filepath);
        //        }

        //        var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Product", filename);
        //        using (var stream = new FileStream(exactpath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return filename;
        //}
    }
}

using Microsoft.AspNetCore.Mvc;
using StationeryStore.Interfaces;
using StationeryStore.Services;

namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : BaseController
    {
        public AttributeController(UserInfoService userInfoService, IUserRepository userRepository) : base(userInfoService, userRepository)
        {
        }
    }
}

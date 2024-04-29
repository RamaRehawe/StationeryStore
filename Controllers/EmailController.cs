using Microsoft.AspNetCore.Mvc;
using StationeryStore.Interfaces;
using StationeryStore.Services;

namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : BaseController
    {

        public EmailController(UserInfoService userInfoService, IUserRepository userRepository) : base(userInfoService, userRepository)
        {
        }
    }
}

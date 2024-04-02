using Microsoft.AspNetCore.Mvc;
using StationeryStore.Interfaces;
using StationeryStore.Models;
using StationeryStore.Services;

namespace StationeryStore.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserInfoService _userInfoService;
        protected readonly IUserRepository _userRepository;

        public BaseController(UserInfoService userInfoService, IUserRepository userRepository)
        {
            _userInfoService = userInfoService;
            _userRepository = userRepository;
        }

        protected User? GetActiveUser()
        {
            var username = _userInfoService.GetUserIdFromToken();

            var user = _userRepository.GetUserByUsernameAsync(username);
            return user;
        }
    }
}

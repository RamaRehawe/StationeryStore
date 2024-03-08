using Microsoft.AspNetCore.Mvc;
using StationeryStore.Data;
using StationeryStore.Interfaces;
using StationeryStore.Models;


namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly DataContext _context;
        public UserController(IUserRepository userRepository, DataContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }

    }
}

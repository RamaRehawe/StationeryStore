using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StationeryStore.Data;
using StationeryStore.Dto;
using StationeryStore.Interfaces;
using StationeryStore.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] User user)
        {
            var existingUser = _userRepository.GetUserByUsernameAsync(user.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists");
            }

            _userRepository.AddUser(user);
            return Ok("User signed up successfully!");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login( LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.Email);

            if (user == null || user.Password != loginDto.Password)
                return Unauthorized("Invalid Username or Password");
            // Generate JWT token
            var token = GenerateJwtToken(user.Email, user.UserType);
            await _userRepository.UpdateTokenByUsernameAsync(loginDto.Email, token);
            _userRepository.Save();
            return Ok(new { Token = token });


        }
        [Authorize(Roles = "Admin")]
        [HttpPost("admin-action")]
        public IActionResult AdminAction()
        {
            // Only users with the "Admin" role can access this endpoint
            return Ok("Admin action performed successfully!");
        }

        private string GenerateJwtToken(string username, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims: new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role)
                },
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

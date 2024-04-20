using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto user)
        {
            var ruser = _mapper.Map<RegisterUserDto>(user);
            ruser.UserType = "Customer";

            var res = this.Register(ruser);
            if (res != null)
                return res;
            return await this.Login(new LoginDto
            {
                Email = user.Email,
                Password = user.Password,
            });
            //return Ok("User signed up successfully!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register_user")]
        public IActionResult RegisterUser([FromBody] RegisterUserDto user)
        {
            var res = this.Register(user);
            if (res != null)
                return res;
            
            return Ok("Done");
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = _userRepository.GetUserByUsernameAsync(loginDto.Email);

            if (user == null || user.Password != loginDto.Password)
                return Unauthorized("Invalid Username or Password");
            // Generate JWT token
            var token = GenerateJwtToken(user.Email, user.UserType);
            await _userRepository.UpdateTokenByUsernameAsync(loginDto.Email, token);
            _userRepository.Save();
            return Ok(new { Token = token });


        }
        //[Authorize(Roles = "Admin")]
        //[HttpPost("admin-action")]
        //public IActionResult AdminAction()
        //{
        //    // Only users with the "Admin" role can access this endpoint
        //    return Ok("Admin action performed successfully!");
        //}

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

        private IActionResult? Register([FromBody] RegisterUserDto user)
        {
            if (user == null)
                return BadRequest(ModelState);

            var newUser = _userRepository.GetUsers()
                .Where(c => c.Email.Trim().ToUpper() == user.Email.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (newUser != null)
            {
                ModelState.AddModelError("", "User allready exists");
                return StatusCode(422, ModelState);
            }
            String[] allowed_types = { "Driver", "Customer", "Item Manager" };
            if (!allowed_types.Contains(user.UserType))
                ModelState.AddModelError("", "Invalid user type");
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(user);
            if (!_userRepository.AddUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            if (userMap.UserType == "Driver")
            {
                var driver = new Driver
                {
                    DriverStatus = true,
                    DriverLicense = "0000",
                    UserId = userMap.Id
                };
                _userRepository.AddDriver(driver);
            }
            return null;
        }



    }
}

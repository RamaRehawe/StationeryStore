using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StationeryStore.Data;
using StationeryStore.Dto;
using StationeryStore.Interfaces;
using StationeryStore.Models;
using StationeryStore.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;

        public UserController(IMapper mapper, IConfiguration configuration,
            UserInfoService userInfoService, 
            IUserRepository userRepository, EmailService emailService) : base(userInfoService, userRepository)
        {
            _configuration  = configuration;
            _mapper = mapper;
            _userRepository = userRepository;
            _emailService = emailService;
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
            // Retrieve the user by email
            var user = _userRepository.GetUserByUsernameAsync(loginDto.Email);

            if (user == null || user.Password != loginDto.Password)
                return Unauthorized("Invalid Username or Password");

            // Generate JWT token
            var token = GenerateJwtToken(user.Email, user.UserType);

            // Update the token for the user in the database
            await _userRepository.UpdateTokenByUsernameAsync(loginDto.Email, token);
            _userRepository.Save();
            // Return the token and user type in the response
            return Ok(new { Token = token, UserType = user.UserType , UserId = user.Id });
        }

        [HttpPost("add_user")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddUser([FromBody] RegisterUserDto userData)
        {
            if (userData.UserType == "Item Manager")
            {
                userData.UserType = "Item Manager";
                var res = this.Register(userData);
                if (res != null)
                    return res;

                return Ok(new { message = "Item manager added successfully!" });
            }
            else if (userData.UserType == "Driver")
            {
                userData.UserType = "Driver";
                var res = this.Register(userData);
                if (res != null)
                    return res;

                return Ok(new { message = "Driver added successfully!" });
            }
            else
            {
                return BadRequest(new { message = "Invalid user type." });
            }
        }

        [HttpPost("update_profile")]
        [Authorize]
        public IActionResult UpdateProfile (ReqUpdateProfileDto profileData)
        {
            var user = base.GetActiveUser()!;
            user.Birthdate = profileData.Birthdate;
            user.Gender = profileData.Gender;
            _userRepository.UpdateUser(user);
            return Ok("User has been updated");
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
                _userRepository.AddDriver(new Driver { DriverStatus = false, UserId = userMap.Id });

            return null;
        }
    }
}

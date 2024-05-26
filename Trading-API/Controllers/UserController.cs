using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Trading_API.Interfaces;
using Trading_API.Requests;
using Trading_API.Resposes;

namespace Trading_API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        public UserController(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest loginRequest)
        {
            var result =await _userRepository.LoginUser(loginRequest);
            if(result == 0)
            {
                return BadRequest("Invalid email or password");
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
            var responseBody = new
            {
                token = token,
                UserId= result
            };
            return Ok(responseBody);
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest registerRequest)
        {
            var result = await _userRepository.RegisterUser(registerRequest);
            if(result==0)
            {
                return BadRequest("User already exists");
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("UserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            var result = await _userRepository.GetUserDetailsAsync();
            if(result==null)
            {
                return BadRequest("User does not exist");
            }
            return Ok(result);
        }
        
        [HttpGet]
        [Route("userrole")]
        
        public async Task<IActionResult> GetUserRoleById(int IdUser)
        {
            var result = await _userRepository.GetUserRoleById(IdUser);
            if(result==null)
            {
                return BadRequest("User does not exist");
            }
            return Ok(result);
        }
        [HttpPatch]
        [Route("updatepassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest updatePasswordRequest)
        {
            var result = await _userRepository.UpdatePassword(updatePasswordRequest);
            if(result==400)
            {
                return BadRequest("WrongSafeWord");
            }
            return Ok("Password changed");
        }
        [HttpGet]
        [Route("usertransactions")]
        public async Task<IActionResult> GetUserTransactions(int IdUser)
        {
            var result = await _userRepository.GetUserTransactions(IdUser);
            if(result==null)
            {
                return BadRequest("User does not exist");
            }
            return Ok(result);
        }
    }
}

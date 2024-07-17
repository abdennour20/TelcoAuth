using Auth.Application.Contracts;
using Auth.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthTelco.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user) {
            _user = user;
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> login([FromBody]  LoginDto loginDto)
        {
            var  result =     await _user.LoginAsync(loginDto);
            return Ok(result);

        }
        [HttpPost("AdminRegister")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterDto registerDto)
        {
            var result =  await _user.RegisterAsync(registerDto);
            return Ok(result);
        }



    }
}
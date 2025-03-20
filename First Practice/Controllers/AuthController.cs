using First_Practice.Models;
using First_Practice.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using First_Practice.Services;
namespace First_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) 
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AuthModel Result = await _authService.RegisterAsync(register) ;
                      
            if(!Result.IsAuthenticated)
            {
                return BadRequest(Result.Message);
            }
            return Ok(Result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AuthModel Result = await _authService.Login(model);

            if (!Result.IsAuthenticated)
            {
                return BadRequest(Result.Message);
            }
            return Ok(Result);
        }
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRole([FromBody] RoleDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
              string Result = await _authService.AddRole(model);
            if (!string.IsNullOrEmpty(Result))
                return BadRequest(Result);


            return Ok(model);
        }


    }
}

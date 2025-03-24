using First_Practice.Models;
using First_Practice.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using First_Practice.Services;
using Serilog;
namespace First_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService,ILogger<AuthController> logger) 
        {
            _authService = authService;
            this._logger = logger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            

            if(!ModelState.IsValid)
            {
                _logger.LogError("The Input is incorrect");
                return BadRequest(ModelState);
            }
            AuthModel Result = await _authService.RegisterAsync(register) ;
                      
            if(!Result.IsAuthenticated)
            {
                _logger.LogError("Something went wrong");
                return BadRequest(Result.Message);
            }
            _logger.LogInformation("Register is completed succssefuly");
            return Ok(Result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("The Input is incorrect");
                return BadRequest(ModelState);
            }
            AuthModel Result = await _authService.Login(model);

            if (!Result.IsAuthenticated)
            {
                _logger.LogError("Something went wrong");
                return BadRequest(Result.Message);
            }
            _logger.LogInformation("Login is completed succssefuly");
            return Ok(Result);
        }
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRole([FromBody] RoleDTO model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("The Input is incorrect");
                return BadRequest(ModelState);
            }
              string Result = await _authService.AddRole(model);
            if (!string.IsNullOrEmpty(Result))
            {
                _logger.LogError("Something went wrong");
                return BadRequest(Result);
            }
            _logger.LogInformation("Add Role is completed succssefuly");
            return Ok(model);
        }


    }
}

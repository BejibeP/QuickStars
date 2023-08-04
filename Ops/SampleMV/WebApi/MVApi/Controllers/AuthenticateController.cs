using Microsoft.AspNetCore.Mvc;
using QuickStars.MVApi.Business.Dtos.Auth;
using QuickStars.MVApi.Business.Interfaces;
using QuickStars.MVApi.Extensions;

namespace QuickStars.MVApi.Controllers
{
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticateController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("api/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Login(dto);
            return this.FromResult(result);
        }

        [Route("api/register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Register(dto);
            return this.FromResult(result);
        }

        [Route("api/resetPassword")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.ResetPassword(dto);
            return this.FromResult(result);
        }

    }
}
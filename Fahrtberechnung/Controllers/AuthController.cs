using Fahrtberechnung.DTOs;
using Fahrtberechnung.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtberechnung.Controllers
{
    [ApiController, Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService authService;
        private readonly IUserService userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
            this.authService = authService;
            this.userService = userService;
        }

        /// <summary>
        /// Authorization
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async ValueTask<IActionResult> Login(UserLoginDto dto)
        {
            var token = await authService.GenerateToken(dto.Login, dto.Password);
            return Ok(new
            {
                token
            });
        }

        [HttpPost("register")]
        public async ValueTask<IActionResult> RegisterAsync(UserCreationDto userForCreationDTO)
        {
            return Ok(await userService.CreateAsync(userForCreationDTO));
        }
    }
}

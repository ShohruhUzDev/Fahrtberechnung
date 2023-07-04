using System.ComponentModel.DataAnnotations;
using Fahrtberechnung.DTOs;
using Fahrtberechnung.Helpers;
using Fahrtberechnung.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtberechnung.Controllers
{
    [ApiController(), Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPut, Authorize(Roles = CustomRole.USER_ROLE)]
        public async ValueTask<IActionResult> UpdateUser([Required] string login,
            [Required] string password, [FromQuery] UserUpdatDto userForUpdateDTO)
            => Ok(await userService.UpdateAsync(login, password, userForUpdateDTO));

        //[HttpPatch("{id}"), Authorize(Roles = CustomRole.ADMIN_ROLE)]
        //public async ValueTask<IActionResult> ChangeRoleAsync(int id, UserRole userRole)
        //    => Ok(await userService.ChangeRoleAsync(id, userRole));

        [HttpPatch("Password")]
        public async ValueTask<IActionResult> ChangePasswordAsync(string oldPassword, string newPassword)
            => Ok(await userService.ChangePasswordAsync(oldPassword, newPassword));

        [HttpGet]
        public async ValueTask<IActionResult> GetAll()
            => Ok(await userService.GetAllAsync());

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
            => Ok(await userService.GetAsync(u => u.Id == id));

        [HttpGet("User"), Authorize(Roles = CustomRole.USER_ROLE)]
        public async ValueTask<IActionResult> GetAsync()
            => Ok(await userService.GetAsync(u => u.Id == HttpContextHelper.UserId));
    }
}

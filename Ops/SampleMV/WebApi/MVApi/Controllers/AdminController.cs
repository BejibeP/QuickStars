﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickStars.MVApi.Business.Dtos.Auth;
using QuickStars.MVApi.Business.Interfaces;
using QuickStars.MVApi.Domain;
using QuickStars.MVApi.Extensions;

namespace QuickStars.MVApi.Controllers
{
    [Authorize(Roles = IdentityRoles.Admin)]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Route("api/forceresetpwd")]
        [HttpPost]
        public async Task<IActionResult> ForceResetPassword([FromBody] ResetPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _adminService.ResetUserPassword(dto);
            return this.FromResult(result);
        }

        [Route("api/addrole")]
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] UserRoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _adminService.AddUserRole(dto);
            return this.FromResult(result);
        }

        [Route("api/deleterole")]
        [HttpPost]
        public async Task<IActionResult> DeleteRole([FromBody] UserRoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _adminService.DeleteUserRole(dto);
            return this.FromResult(result);
        }

    }
}
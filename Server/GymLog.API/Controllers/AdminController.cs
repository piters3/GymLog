using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymLog.API.DTOs;
using GymLog.API.Entities;
using GymLog.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymLog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IAdminRepository _repo;
        private readonly IMapper _mapper;

        public AdminController(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper, IAdminRepository repo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await _repo.GetUsersWithRoles();

            return Ok(userList);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var rolesModel = _mapper.Map<IEnumerable<RoleDto>>(roles);

            return Ok(rolesModel);
        }

        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUserWithRoles(id);
            var userToReturn = _mapper.Map<UserDetailsDto>(user);

            return Ok(userToReturn);
        }

        [HttpPost("updateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDetailsDto userForUpdate)
        {
            var user = await _repo.GetUser(id);
            var userRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = userForUpdate.Roles.Select(r => r.Name).ToArray();

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to remove the roles");

            _mapper.Map(userForUpdate, user);
            await _repo.UpdateAsync(user);

            return Accepted();
        }
    }
}
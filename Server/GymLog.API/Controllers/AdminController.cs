using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymLog.API.Data;
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
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUsersRepository _repo;
        private readonly IMapper _mapper;

        public AdminController(DataContext context, UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper, IUsersRepository repo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _repo = repo;
            _context = context;
        }

        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await (from user in _context.Users
                                  orderby user.UserName
                                  select new
                                  {
                                      user.Id,
                                      user.UserName,
                                      Roles = (from userRole in user.UserRoles
                                               join role in _context.Roles
                                               on userRole.RoleId
                                               equals role.Id
                                               select role.Name).ToList()
                                  }).ToListAsync();
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
            await _repo.SaveAllAsync();

            return Accepted();
        }
    }
}
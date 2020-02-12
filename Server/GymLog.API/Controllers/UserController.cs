using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GymLog.API.DTOs;
using GymLog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        /// <summary>
        /// Gets all users's daylogs.
        /// </summary> 
        /// <returns>A collecion of user's daylogs</returns>
        /// <response code="200">Returns collection of daylogs</response> 
        [HttpGet("daylogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserDaylogs()
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var daylogs = await _repo.GetUserDaylogs(id);
            var result = _mapper.Map<ICollection<DaylogDto>>(daylogs);

            return Ok(result);
        }

        //[HttpGet("{id}", Name = "GetUser")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    var user = await _repo.GetUser(id);

        //    if (user is null)
        //    {
        //        return NotFound();
        //    }

        //    var userToReturn = _mapper.Map<UserDetailsDto>(user);

        //    return Ok(userToReturn);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUser(int id, UserDetailsModel userForUpdate)
        //{
        //    if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //        return Unauthorized();

        //    var userFromRepo = await _repo.GetUser(id);

        //    _mapper.Map(userForUpdate, userFromRepo);

        //    if (await _repo.SaveAll())
        //        return NoContent();

        //    throw new Exception($"Updating user {id} failed on save");
        //}
    }
}
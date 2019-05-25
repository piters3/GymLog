using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GymLog.API.Data;
using GymLog.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGymLogRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IGymLogRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();

            var usersToReturn = _mapper.Map<IEnumerable<UserDetailsModel>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserDetailsModel>(user);

            return Ok(userToReturn);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        //{
        //    if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //        return Unauthorized();

        //    var userFromRepo = await _repo.GetUser(id, true);

        //    _mapper.Map(userForUpdateDto, userFromRepo);

        //    if (await _repo.SaveAll())
        //        return NoContent();

        //    throw new Exception($"Updating user {id} failed on save");
        //}
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GymLog.API.Models;
using GymLog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GymLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _repo.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserDetailsDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _repo.GetUser(id);

            if (user is null)
            {
                return NotFound();
            }

            var userToReturn = _mapper.Map<UserDetailsDto>(user);

            return Ok(userToReturn);
        }


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
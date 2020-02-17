using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GymLog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutinesController : ControllerBase
    {
        private readonly IRoutinesRepository _repo;
        private readonly IMapper _mapper;

        public RoutinesController(IRoutinesRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all users's routines.
        /// </summary> 
        /// <returns>A collecion of user's routines</returns>
        /// <response code="200">Returns collection of routines</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDaylogsDates()
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var routines = await _repo.GetRoutines(id);

            return Ok(routines);
        }
    }
}
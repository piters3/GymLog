using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GymLog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaylogsController : ControllerBase
    {
        private readonly IDaylogsRepository _repo;

        public DaylogsController(IDaylogsRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Gets all users's daylogs.
        /// </summary> 
        /// <returns>A collecion of user's daylogs</returns>
        /// <response code="200">Returns collection of daylogs</response> l
        [HttpGet("daylogsDates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDaylogsDates(DateTime date)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ICollection<DateTime> dates = await _repo.GetDaylogsDates(id, date);

            return Ok(dates);
        }
    }
}
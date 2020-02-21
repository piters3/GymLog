using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GymLog.API.DTOs;
using GymLog.API.Entities;
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
        private readonly IMapper _mapper;

        public DaylogsController(IDaylogsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all users's daylogs.
        /// </summary> 
        /// <returns>A collecion of user's daylogs</returns>
        /// <response code="200">Returns collection of daylogs</response>
        [HttpGet("daylogsDates/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDaylogsDates(DateTime date)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ICollection<DateTime> dates = await _repo.GetDaylogsDates(id, date);

            return Ok(dates);
        }

        /// <summary>
        /// Find daylog by Id.
        /// </summary>
        /// <param name="id"></param>  
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            DaylogDto daylog = await _repo.GetDaylog(id);

            return Ok(daylog);
        }

        [HttpGet("{date:DateTime}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDaylog(DateTime date)
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            DaylogDto daylog = await _repo.GetDaylog(id, date);

            return Ok(daylog);
        }

        /// <summary>
        /// Adds new daylog.
        /// </summary>
        /// <param name="daylogDto"></param>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Post(DaylogDto daylogDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var daylog = _mapper.Map<Daylog>(daylogDto);
            daylog.UserId = id;

            await _repo.AddAsync(daylog);

            return CreatedAtAction(nameof(Get), new { id = daylog.Id }, daylog);
        }
    }
}
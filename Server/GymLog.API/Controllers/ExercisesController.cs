using System.Collections.Generic;
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
    public class ExercisesController : ControllerBase
    {
        private readonly IExercisesRepository _exercisesRepository;
        private readonly IMapper _mapper;

        public ExercisesController(IExercisesRepository exercisesRepository, IMapper mapper)
        {
            _exercisesRepository = exercisesRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all exercises.
        /// </summary> 
        /// <returns>A collecion of Exercise</returns>
        /// <response code="200">Returns collection of Exercise</response>     
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var exercises = await _exercisesRepository.GetAllAsync();
            var result = _mapper.Map<ICollection<ExerciseDto>>(exercises);

            return Ok(result);
        }

        /// <summary>
        /// Find muscle by Id.
        /// </summary>
        /// <param name="id"></param>  
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var exercise = await _exercisesRepository.GetAsync(id);

            if (exercise is null)
                return NotFound();

            var result = _mapper.Map<ExerciseDto>(exercise);

            return Ok(result);
        }
    }
}
using System.Collections.Generic;
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
    public class MusclesController : ControllerBase
    {
        private readonly IMusclesRepository _musclesRepository;
        private readonly IMapper _mapper;

        public MusclesController(IMusclesRepository musclesRepository, IMapper mapper)
        {
            _musclesRepository = musclesRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all muscles.
        /// </summary> 
        /// <returns>A collecion of Muscle</returns>
        /// <response code="200">Returns collection of Muscle</response>
        /// <response code="404">If the collection is empty</response>         
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var muscles = await _musclesRepository.GetAllAsync();

            if (muscles is null || muscles.Count == 0)
                return NotFound();

            var result = _mapper.Map<IEnumerable<MuscleDto>>(muscles);

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
            var muscle = await _musclesRepository.GetAsync(id);

            if (muscle is null)
                return NotFound();

            var result = _mapper.Map<MuscleDto>(muscle);

            return Ok(result);
        }

        /// <summary>
        /// Adds new muscle.
        /// </summary>
        /// <param name="muscleDto"></param>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Post(MuscleDto muscleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var muscle = _mapper.Map<Muscle>(muscleDto);

            await _musclesRepository.AddAsync(muscle);

            return CreatedAtAction(nameof(Get), new { id = muscle.Id }, muscle);
        }

        /// <summary>
        /// Updates muscle from body data.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="muscleDto"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Update(int id, MuscleDto muscleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != muscleDto.Id)
                return BadRequest();

            var muscle = await _musclesRepository.GetAsync(id);

            if (muscle is null)
                return NotFound();

            _mapper.Map(muscleDto, muscle);

            await _musclesRepository.UpdateAsync(muscle);

            return Accepted();
        }

        /// <summary>
        /// Deletes muscle by Id
        /// </summary>
        /// <param name="id"></param>  
        /// <response code="202">Successful delete</response>
        /// <response code="404">Muscle not found</response>  
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var muscle = await _musclesRepository.GetAsync(id);

            if (muscle is null)
                return NotFound();

            await _musclesRepository.DeleteAsync(muscle);

            return Accepted();
        }
    }
}
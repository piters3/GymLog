using System.Threading.Tasks;
using GymLog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GymLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusclesController : ControllerBase
    {
        private readonly IMusclesRepository _musclesRepository;

        public MusclesController(IMusclesRepository musclesRepository)
        {
            _musclesRepository = musclesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var muscles = await _musclesRepository.GetAllAsync();

            if (muscles is null || muscles.Count == 0)
            {
                return NotFound();
            }

            return Ok(muscles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var muscle = await _musclesRepository.GetAsync(id);

            if (muscle is null)
            {
                return NotFound();
            }

            return Ok(muscle);
        }
    }
}
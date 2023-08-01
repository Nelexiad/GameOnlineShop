using Entities.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories.Repositories;

namespace ApiVideogameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideogamesController : ControllerBase
    {

        private VideogameRepositoryAPI _repository;

        public VideogamesController(VideogameRepositoryAPI repo)
        {
            _repository = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var videogames = await _repository.GetAll();
            return Ok(videogames);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var videogames = await _repository.Get(id);
            return Ok(videogames);
        }
        [HttpPost]
        public async Task<IActionResult> Create(VideogameDTO videogameDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdVideogameDTO = await _repository.Create(videogameDTO);
                return Ok(createdVideogameDTO);
                //return CreatedAtAction(nameof(Get), null, createdVideogameDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the videogame.");
                throw;
            }
        }
    }
}

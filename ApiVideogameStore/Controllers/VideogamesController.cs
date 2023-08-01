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
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BaseRepositories<Videogame> _videogameRepository;

        public HomeController(ILogger<HomeController> logger, BaseRepositories<Videogame> videogameRepository)
        {
            _logger = logger;
            _videogameRepository = videogameRepository;
        }

        public IActionResult Index(int id)
        {
            
            return View();
        }

        public async Task<IActionResult> GetVideogames(int id)
        {
            Videogame videogame = await _videogameRepository.Get(id);
            return View(videogame);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
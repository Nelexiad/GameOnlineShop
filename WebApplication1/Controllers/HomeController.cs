using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Models.MappedModel;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BaseRepositories<Videogame,VideogameDTO> _videogameRepository;

        public HomeController(ILogger<HomeController> logger, BaseRepositories<Videogame, VideogameDTO> videogameRepository)
        {
            _logger = logger;
            _videogameRepository = videogameRepository;
        }

        public IActionResult Index(int id)
        {
            
            return View();
        }

        
    }
}
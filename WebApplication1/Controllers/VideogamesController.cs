using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories;
using Services;

namespace WebApplication1.Controllers
{
    public class VideogamesController : Controller
    {
        private VideogameServices _services;

        public VideogamesController(VideogameServices repo)
        {
            _services = repo;



        }




        // GET: Videogames
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var url = "https://localhost:7115/api/Videogames";
            var videogames = await _services.GetAll( url);
            return View(videogames);
        }

        public async Task<IActionResult> Create()
        {
            var genres = new List<GenreDTO>
    {
        new GenreDTO { Id = 1, GenreName = "Action" },
        new GenreDTO { Id = 2, GenreName = "Fps" },
        new GenreDTO { Id = 3, GenreName = "Horror" },
        // Altri generi...
    };

            ViewBag.Genres = new SelectList(genres, "Id", "GenreName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VideogameDTO videogame)
        {
            string url = "https://localhost:7114/api/Videogames";
            await _services.Create(videogame,url);
            return View();
        }

    }
}

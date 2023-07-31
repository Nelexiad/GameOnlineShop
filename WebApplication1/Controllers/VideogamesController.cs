using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Models.MappedModel;

namespace WebApplication1.Controllers
{
    public class VideogamesController :  Controller
    {
        private VideogameRepository _repository;

        public VideogamesController(VideogameRepository repo)
        {
            _repository = repo;
           
           

        }

        


        // GET: Videogames
        public async Task<IActionResult> Index()
        {
            var videogames = await _repository.GetAll();
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
           await  _repository.Create(videogame);
            return View();
        }
        
    }
}

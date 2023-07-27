using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class VideogamesController :  Controller
    {
        private readonly VideogameRepository _repository;

        public VideogamesController(VideogameRepository repo)
        {
            _repository = repo;
        }



        // GET: Videogames
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await _repository.GetAll();
            return View(applicationDbContext);
        }

        
    }
}

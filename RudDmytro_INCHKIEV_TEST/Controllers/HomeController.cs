using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RudDmytro_INCHKIEV_TEST.Models;

namespace RudDmytro_INCHKIEV_TEST.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public HomeController(ILogger<HomeController> logger, MyDbContext context, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpGet]
        
        public async Task<IActionResult> Books(string name)
        {
            ViewData["CurrentFilter"] = name;
            if (name!=null)
            return View( await _context.Books.Where(q => q.title.Contains(name)).ToListAsync());

            return View(await _context.Books.ToListAsync());
        }
        [HttpGet]
        public IActionResult Download(int id)
        {
           var book= _context.Books.FirstOrDefault(q => q.id == id);
            var filepath = Path.Combine("~/books", book.url);
            return  File(filepath, "text/plain", book.url);
        }

        public async Task<IActionResult> Download(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //TODO Downloads

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.id == id);
            return  RedirectToAction("Books", "Home");
        }


    }
}

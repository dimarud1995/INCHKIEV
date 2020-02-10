using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RudDmytro_INCHKIEV_TEST.Models;

namespace RudDmytro_INCHKIEV_TEST.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class BooksController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BooksController(MyDbContext context,IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Books
      
        
            
        public async Task<IActionResult> Index()
        {
           
                return View(await _context.Books.ToListAsync());
           
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFilename = null;
                if (model.bookFile != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "books");
                    uniqueFilename = Guid.NewGuid().ToString() + "_" + model.bookFile.FileName;
                    string filePath=Path.Combine(uploadsFolder, uniqueFilename);
                    await model.bookFile.CopyToAsync( new FileStream(filePath, FileMode.Create));

                }
                Book newBook = new Book
                {
                    title = model.title,
                    author = model.author,
                    url = uniqueFilename,
                    downloads = 0,
                    year = model.year
                };

                _context.Add(newBook);

                await _context.SaveChangesAsync();
                return RedirectToAction("details",new { id=newBook.id});
            }
            return View(model);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                BookViewModel bookVM = new BookViewModel
                {
                    title = book.title,
                    author = book.author,
                    year = book.year,
                    bookFile = null
            };
                return View(bookVM);
            }

           
        }

       
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, BookViewModel model)
        {
            if (await _context.Books.FindAsync(id)==null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var book = await _context.Books.FindAsync(id);
                string uniqueFilename = null;
                    if (model.bookFile != null)
                    {
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "books");
                        uniqueFilename = Guid.NewGuid().ToString() + "_" + model.bookFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                        await model.bookFile.CopyToAsync(new FileStream(filePath, FileMode.Create));

                }
                else
                {
                    return View(model);
                }

                book.url = uniqueFilename;
                book.title = model.title;
                book.year = model.year;
                book.author = model.author;
                
                _context.Update(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("details", new { book.id });
             
            }
            return View(model);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
      
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "books");
            string filePath = Path.Combine(uploadsFolder, book.url);
            System.IO.File.Delete(filePath);
            

            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.id == id);
        }
    }
}

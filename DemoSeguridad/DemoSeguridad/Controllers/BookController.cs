using DemoSeguridad.Authorization;
using DemoSeguridad.Data;
using DemoSeguridad.Data.Entities;
using DemoSeguridad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace DemoSeguridad.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public BookController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public IActionResult BookList()
        {
            var books = _context.Books.Include(book => book.Author).ToList();
            var model = ModelConverter.GetBookListViewModel(books);
            return View(model);
        }

        [Authorize("Libros.Agregar")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize("Libros.Agregar")]
        [HttpPost]
        public IActionResult Create(BookCreateModel book)
        {
            try
            {
                if (ModelState.IsValid && book != null && book.AuthorName != null)
                {
                    var author = _context.Authors.FirstOrDefault(author => author.FirstName == book.AuthorName);
                    if (author != null)
                    {
                        Book bookCreate = new()
                        {
                            Title = book.Title,
                            Description = book.Description,
                            Content = book.Content,
                            Author = author
                        };

                        _context.Books.Add(bookCreate);
                        _context.SaveChanges();
                        return RedirectToAction("BookList");
                    } else
                    {
                        ModelState.AddModelError("", "El autor indicado no fue encontrado");
                    }
                }
                return View(book);
            }
            catch
            {
                ModelState.AddModelError("", "Se produjo un error inesperado, intente mas tarde");
                return View();
            }
        }

        [Authorize("Libros.Eliminar")]
        public IActionResult Delete(long id)
        {
            if (id < 1)
            {
                return new BadRequestResult();
            }
            var book = _context.Books.FirstOrDefault(book => book.Id == id);
            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("BookList");
        }

        [Authorize("Libros.Leer")]
        public IActionResult Details(long id)
        {
            var book = _context.Books.Include(book => book.Author).FirstOrDefault(book => book.Id == id);
            if (book != null)
            {
                return View(ModelConverter.GetBookViewModel(book));
            } else
            {
                return new BadRequestResult();
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingTracker.Models;
using ReadingTracker.Services;
using System.Security.Claims;

namespace ReadingTracker.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly JsonService _jsonService;

        public BooksController(JsonService jsonService)
        {
            _jsonService = jsonService;
        }

        [HttpGet("/books")]
        public IActionResult Index(string? status)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var books = _jsonService.GetBooks()
                .Where(b => b.AppUserId == userId);

            if (status == "oxunur")
                books = books.Where(b => b.Status == BookStatus.Oxunur);

            if (status == "bitirilib")
                books = books.Where(b => b.Status == BookStatus.Bitirilib);

            ViewBag.CurrentStatus = status;

            return View(books.ToList());
        }

        [HttpGet("/books/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("/books/{id:int}")]
        public IActionResult Details(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var book = _jsonService.GetBooks()
                .FirstOrDefault(b => b.Id == id && b.AppUserId == userId);

            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost("/books/create")]
        public IActionResult Create(BookCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var books = _jsonService.GetBooks();

            var newBook = new Book
            {
                Id = books.Count == 0 ? 1 : books.Max(b => b.Id) + 1,
                Title = model.Title,
                Author = model.Author,
                Status = BookStatus.Oxunur,
                AppUserId = userId
            };

            books.Add(newBook);
            _jsonService.SaveBooks(books);

            return RedirectToAction("Index");
        }

        [HttpPost("/books/finish/{id}")]
        public IActionResult Finish(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var books = _jsonService.GetBooks();

            var book = books.FirstOrDefault(b => b.Id == id && b.AppUserId == userId);

            if (book == null)
                return NotFound();

            if (book.Status == BookStatus.Bitirilib)
                return RedirectToAction("Details", new { id });

            book.Status = BookStatus.Bitirilib;
            _jsonService.SaveBooks(books);

            return RedirectToAction("Details", new { id });
        }
    }
}

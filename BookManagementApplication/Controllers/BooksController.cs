using BookManagementApplication.Service;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookManagementApplication.Models;
using System.Diagnostics;

namespace BookManagementApplication.Controllers
{
    public class BooksController(BookService bookService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var owners = await bookService.GetOwnersAsync();
            if (owners == null || !owners.Any())
            {
                return View(new Dictionary<string, List<BookViewModel>>());
            }

            var books = owners.SelectMany(o => o.Books.Select(b => new BookViewModel { Name = b.Name, Type = b.Type, OwnerAge = o.Age })).ToList();

            var groupedBooks = books
                .GroupBy(b => b.OwnerAge >= 18 ? "Books owned by Adults" : "Books owned by Children")
                .ToDictionary(g => g.Key, g => g.OrderBy(b => b.Name).ToList());

            return View(groupedBooks);
        }

        public async Task<IActionResult> HardcoverOnly()
        {
            var owners = await bookService.GetOwnersAsync();
            if (owners == null || !owners.Any())
            {
                return View("Index", new Dictionary<string, List<BookViewModel>>());
            }

            var books = owners.SelectMany(o => o.Books.Where(b => b.Type == "Hardcover").Select(b => new BookViewModel { Name = b.Name, Type = b.Type, OwnerAge = o.Age })).ToList();

            var groupedBooks = books
                .GroupBy(b => b.OwnerAge >= 18 ? "Hardcover Books owned by Adults" : "Hardcover Books owned by Children")
                .ToDictionary(g => g.Key, g => g.OrderBy(b => b.Name).ToList());

            return View("Index", groupedBooks);
        }
    }
}
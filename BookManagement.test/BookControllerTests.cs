using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using BookManagement.test.Model;
using BookManagement.test.Service;


namespace BookManagement.test
{
    [TestClass]
    public class BookControllerTests
    {
        private BookService _bookService;
        private List<Owner> _owners;

        [TestInitialize]
        public void Setup()
        {
            _bookService = new BookService(new HttpClient());
            _owners = new List<Owner>
        {
            new Owner { Name = "Jane", Age = 23, Books = new List<Book> { new Book { Name = "Hamlet", Type = "Hardcover" }, new Book { Name = "Wuthering Heights", Type = "Paperback" } } },
            new Owner { Name = "Charlotte", Age = 14, Books = new List<Book> { new Book { Name = "Hamlet", Type = "Paperback" } } },
            new Owner { Name = "Max", Age = 25, Books = new List<Book> { new Book { Name = "React: The Ultimate Guide", Type = "Hardcover" }, new Book { Name = "Gulliver's Travels", Type = "Hardcover" }, new Book { Name = "Jane Eyre", Type = "Paperback" }, new Book { Name = "Great Expectations", Type = "Hardcover" } } },
            new Owner { Name = "William", Age = 15, Books = new List<Book> { new Book { Name = "Great Expectations", Type = "Hardcover" } } },
            new Owner { Name = "Charles", Age = 17, Books = new List<Book> { new Book { Name = "Little Red Riding Hood", Type = "Hardcover" }, new Book { Name = "The Hobbit", Type = "Ebook" } } }
        };
        }

        [TestMethod]
        public void GetBooksByCategory_HardcoverOnly_ShouldFilterAndSortBooks()
        {
            var result = _bookService.GetBooksByCategory(_owners, true);

            // Debugging: Print the result
            foreach (var category in result.Keys)
            {
                Console.WriteLine($"{category}: {string.Join(", ", result[category])}");
            }

            Assert.AreEqual(4, result["Books owned by Adults"].Count);
            Assert.AreEqual(2, result["Books owned by Children"].Count);
        }
        [TestMethod]
        public void GetBooksByCategory_ShouldGroupAndSortBooks()
        {
            var result = _bookService.GetBooksByCategory(_owners);

            // Debugging: Print the result
            foreach (var category in result.Keys)
            {
                Console.WriteLine($"{category}: {string.Join(", ", result[category])}");
            }

            Assert.AreEqual(6, result["Books owned by Adults"].Count);
            Assert.AreEqual(4, result["Books owned by Children"].Count);
        }
    }
}

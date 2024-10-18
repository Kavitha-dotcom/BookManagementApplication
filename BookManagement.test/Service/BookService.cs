using BookManagement.test.Model;

using BookManagementApplication.Service;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookManagement.test.Service
{
    public class BookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Owner>> FetchBookAsync(string apiUrl)
        {
            var response = await _httpClient.GetStringAsync("https://digitalcodingtest.bupa.com.au/api/v1/bookowners");
            return JsonConvert.DeserializeObject<List<Owner>>(response);

        }

        public Dictionary<string, List<string>> GetBooksByCategory(List<Owner> owners, bool hardcoverOnly = false)
        {
            var booksByCategory = new Dictionary<string, List<string>>
        {
            { "Books owned by Adults", new List<string>() },
            { "Books owned by Children", new List<string>() }
        };

            foreach (var owner in owners)
            {
                var category = owner.Age >= 18 ? "Books owned by Adults" : "Books owned by Children";
                var books = owner.Books
                    .Where(b => !hardcoverOnly || b.Type == "Hardcover")
                    .Select(b => b.Name)
                    .OrderBy(b => b)
                    .ToList();

                booksByCategory[category].AddRange(books);
            }

            return booksByCategory;
        }
    }
}
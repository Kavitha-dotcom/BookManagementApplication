using BookManagement.test.Model;

using BookManagementApplication.Service;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
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
        public async Task<List<Owner>> GetOwnersAsync()
        {
            var response = await _httpClient.GetAsync("https://digitalcodingtest.bupa.com.au/api/v1/bookowners");

            if (!response.IsSuccessStatusCode)
            {
                // Handle non-success status codes
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return new List<Owner>(); // Return an empty list for Bad Request
                }

                response.EnsureSuccessStatusCode(); // Throw exception for other status codes
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(responseContent))
            {
                return new List<Owner>();
            }

            var owners = JsonConvert.DeserializeObject<List<Owner>>(responseContent);
            return owners ?? new List<Owner>();

        }

        public Dictionary<string, List<string>> GetBooksByCategory(List<Owner> owners, bool hardcoverOnly = false)
        {
            if (owners == null)
            {
                throw new ArgumentNullException(nameof(owners));
            }

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
using BookManagementApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookManagementApplication.Service
{
        public class BookService
        {
            private readonly HttpClient _httpClient;

            public BookService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<List<Owner>> GetOwnersAsync()
            {
                var response = await _httpClient.GetAsync("https://digitalcodingtest.bupa.com.au/api/v1/bookowners");
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(responseContent))
                {
                    return new List<Owner>();
                }

                var owners = JsonConvert.DeserializeObject<List<Owner>>(responseContent);
                return owners ?? new List<Owner>();

            }
        }
    }
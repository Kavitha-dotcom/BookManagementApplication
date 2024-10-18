using System.Text.Json.Serialization;

namespace BookManagementApplication.Models
{
    public class Owner
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }

        public List<Book> Books { get; set; }
    }
}


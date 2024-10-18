using System.Text.Json.Serialization;

namespace BookManagementApplication.Models
{
    public class Book
    {
        [JsonPropertyName("Name")]
        public string? Name { get; set; }
        [JsonPropertyName("Type")]
        public string? Type { get; set; }
    }
}

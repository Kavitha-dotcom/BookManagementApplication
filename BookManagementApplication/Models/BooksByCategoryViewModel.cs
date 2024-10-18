namespace BookManagementApplication.Models
{
    public class BooksByCategoryViewModel
    {
        public string Category { get; set; }
        public List<BookViewModel> Books { get; set; }
    }
}

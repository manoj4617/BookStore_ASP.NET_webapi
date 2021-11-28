using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BooksModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title of the book is required")]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

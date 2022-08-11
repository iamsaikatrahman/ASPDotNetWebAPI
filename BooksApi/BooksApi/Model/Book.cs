using System.ComponentModel.DataAnnotations;

namespace BooksApi.Model
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Book Title.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Please enter author name.")]
        public string? AuthorName { get; set; }
        [Required(ErrorMessage = "Please enter short description")]
        [MaxLength(250)]
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

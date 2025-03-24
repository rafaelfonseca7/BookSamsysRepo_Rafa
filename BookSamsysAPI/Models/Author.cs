using System.ComponentModel.DataAnnotations;

namespace BookSamsysAPI.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name must be beetween 1 and 50 characters.")]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}


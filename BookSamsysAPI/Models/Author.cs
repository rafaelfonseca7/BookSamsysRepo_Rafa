using System.ComponentModel.DataAnnotations;

namespace BookSamsysAPI.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

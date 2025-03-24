using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSamsysAPI.Models
{
    public class Book
    {

        [Key]
        public string Isbn { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; } = false;

        public virtual Author Author { get; set; }

    }
}

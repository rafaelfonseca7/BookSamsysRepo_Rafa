using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSamsysAPI.Models
{
    public class Book
    {

        [Key]    
        [RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$", ErrorMessage = "Invalid ISBN format.")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "ISBN must be 10 or 13 characters long.")]
        public string Isbn { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Title must be beetween 1 and 20 characters.")] 
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

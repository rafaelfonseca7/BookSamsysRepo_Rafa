using System.ComponentModel.DataAnnotations;

namespace BookSamsysAPI.DTOs
{
    public class CreateBookDTO
    {
        [RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$", ErrorMessage = "Invalid ISBN format.")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "ISBN must be 10 or 13 characters long.")]
        [Required]
        public string Isbn { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Title must be between 1 and 20 characters.")]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}

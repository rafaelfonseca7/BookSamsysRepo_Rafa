using System.ComponentModel.DataAnnotations;

namespace BookSamsysAPI.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name must be beetween 1 and 50 characters.")]
        public string Name { get; set; }
    }
}

namespace BookSamsysAPI.DTOs
{
    public class CreateBookDTO
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public decimal Price { get; set; }
    }
}

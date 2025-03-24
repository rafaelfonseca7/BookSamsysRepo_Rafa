namespace BookSamsysAPI.DTOs
{
    public class BookDTO
    {
        public string Isbn { get; set; }
        public string Title { get; set; }

        public string AuthorName { get; set; }
        public decimal Price { get; set; }
    }
}

using BookSamsysAPI.DTOs;
using BookSamsysAPI.Models;
using BookSamsysAPI.Repositories;

namespace BookSamsysAPI.Services
{
    public class AuthorService
    {
        private readonly AuthorRepository _repository;

        public AuthorService(AuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync()
        {
            var authors = await _repository.GetAllAsync();
            var authorDTOs = new List<AuthorDTO>();

            foreach (var author in authors)
            {
                authorDTOs.Add(new AuthorDTO { Id = author.Id, Name = author.Name });
            }

            return authorDTOs;
        }

        public async Task<AuthorDTO> GetAuthorByIdAsync(int id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null) return null;

            return new AuthorDTO { Id = author.Id, Name = author.Name };
        }

        public async Task<IEnumerable<BookDTO>> GetBooksFromAuthorAsync(int id)
        {
            var books = await _repository.GetBooksFromAuthor(id);
            var bookDTOs = new List<BookDTO>();

            foreach (var book in books)
            {
                bookDTOs.Add(new BookDTO { Isbn = book.Isbn, Title = book.Title, AuthorId = book.Author.Id, AuthorName = book.Author.Name, Price = book.Price });
            }
            return bookDTOs;
        }

        public async Task<AuthorDTO> AddAuthorAsync(AuthorDTO authorDTO)
        {
            var author = new Author { Name = authorDTO.Name };
            await _repository.AddAsync(author);
            return new AuthorDTO { Id = author.Id, Name = author.Name };
        }
    }
}

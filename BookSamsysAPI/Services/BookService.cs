using BookSamsysAPI.Repositories;
using BookSamsysAPI.Models;
using BookSamsysAPI.DTOs;   

namespace BookSamsysAPI.Services
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;
        private readonly AuthorRepository _authorRepository;

        public BookService(BookRepository bookRepository, AuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            var bookDTOs = new List<BookDTO>();
            foreach (var book in books)
            {
                bookDTOs.Add(new BookDTO { Isbn = book.Isbn, Title = book.Title, AuthorName = book.Author.Name, Price = book.Price });
            }
            return bookDTOs;
        }

        public async Task<BookDTO> GetBookByIsbnAsync(string isbn)
        {
            var book = await _bookRepository.GetByIsbnAsync(isbn);
            if (book == null) return null;

            return new BookDTO { Isbn = book.Isbn, Title = book.Title, AuthorName = book.Author.Name, Price = book.Price };
        }

        public async Task<CreateBookDTO> AddBookAsync(CreateBookDTO createBookDTO)
        {
            var author = await _authorRepository.GetByIdAsync(createBookDTO.AuthorId);
            if (author == null) return null;

            var book = new Book { Isbn = createBookDTO.Isbn, Title = createBookDTO.Title, AuthorId = createBookDTO.AuthorId, Price = createBookDTO.Price };
            
            await _bookRepository.AddAsync(book);
            
            return new CreateBookDTO { Isbn = book.Isbn, Title = book.Title, AuthorId = book.AuthorId, Price = book.Price };
        }

        public async Task UpdateBookAsync(CreateBookDTO updateBookDTO)
        {
            var book = await _bookRepository.GetByIsbnAsync(updateBookDTO.Isbn);
            if (book == null) return;

            book.Title = updateBookDTO.Title;
            book.AuthorId = updateBookDTO.AuthorId;
            book.Price = updateBookDTO.Price;

            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(string isbn)
        {
            await _bookRepository.DeleteAsync(isbn);
        }
    }
}

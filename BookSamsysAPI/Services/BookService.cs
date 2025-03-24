using BookSamsysAPI.Repositories;
using BookSamsysAPI.Models;
using BookSamsysAPI.DTOs;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<BookDTO>> GetBookByTitleAsync(string title)
        {
            var books = await _bookRepository.GetByTitleAsync(title);
            var bookDTOs = new List<BookDTO>();
            foreach (var book in books)
            {
                bookDTOs.Add(new BookDTO { Isbn = book.Isbn, Title = book.Title, AuthorName = book.Author.Name, Price = book.Price });
            }

            return bookDTOs;
        }

        public async Task<CreateBookDTO> AddBookAsync(CreateBookDTO createBookDTO){
            var author = await _authorRepository.GetByIdAsync(createBookDTO.AuthorId);
            if (author == null) 
                throw new KeyNotFoundException("Author not found.");

            var book = new Book 
            { 
                Isbn = createBookDTO.Isbn, 
                Title = createBookDTO.Title, 
                AuthorId = createBookDTO.AuthorId, 
                Price = createBookDTO.Price 
            };

            try
            {
                await _bookRepository.AddAsync(book);
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("duplicate key") == true)
            {
                throw new InvalidOperationException("ISBN already exists.", ex);
            }

            return new CreateBookDTO 
            { 
                Isbn = book.Isbn, 
                Title = book.Title, 
                AuthorId = book.AuthorId, 
                Price = book.Price 
            };
        }


        public async Task UpdateBookAsync(CreateBookDTO updateBookDTO)
        {
            var book = await _bookRepository.GetByIsbnAsync(updateBookDTO.Isbn);
            if (book == null)
                throw new KeyNotFoundException("Book not found.");

            var author = await _authorRepository.GetByIdAsync(updateBookDTO.AuthorId);
            if (author == null)
                throw new KeyNotFoundException("Author not found.");

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

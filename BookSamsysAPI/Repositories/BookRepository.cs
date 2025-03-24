using BookSamsysAPI.Data;
using BookSamsysAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSamsysAPI.Repositories
{
    public class BookRepository
    {
        private readonly BookSamsysContext _context;

        public BookRepository(BookSamsysContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Where(b => !b.IsDeleted).Include(b => b.Author).ToListAsync();
        }

        public async Task<Book> GetByIsbnAsync(string isbn)
        {
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Isbn == isbn);
        }

        public async Task<IEnumerable<Book>> GetByTitleAsync(string title)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(title) && !b.IsDeleted)
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string isbn)
        {
            var book = await GetByIsbnAsync(isbn);
            book.IsDeleted = true;
            await UpdateAsync(book);
        }
    }
}

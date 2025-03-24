using BookSamsysAPI.Data;
using BookSamsysAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSamsysAPI.Repositories
{
    public class AuthorRepository
    {
        private readonly BookSamsysContext _context;

        public AuthorRepository(BookSamsysContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }
    }
}

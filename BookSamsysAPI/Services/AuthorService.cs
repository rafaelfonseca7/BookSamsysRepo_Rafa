using BookSamsysAPI.DTOs;
using BookSamsysAPI.Models;
using BookSamsysAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<AuthorDTO> AddAuthorAsync(AuthorDTO authorDTO)
        {
            var author = new Author { Name = authorDTO.Name };
            await _repository.AddAsync(author);
            return new AuthorDTO { Id = author.Id, Name = author.Name };
        }
    }
}

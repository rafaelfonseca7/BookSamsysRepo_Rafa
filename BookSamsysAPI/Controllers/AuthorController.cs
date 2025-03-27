using BookSamsysAPI.DTOs;
using BookSamsysAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSamsysAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _service;

        public AuthorController(AuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAll()
        {
            return Ok(await _service.GetAllAuthorsAsync());
        }

        [HttpGet("id")]
        public async Task<ActionResult<AuthorDTO>> GetById([FromQuery] int id)
        {
            var author = await _service.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpGet("books")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksFromAuthor([FromQuery] int id)
        {
            var books = await _service.GetBooksFromAuthorAsync(id);
            if (books == null) return NotFound();
            return Ok(books);
        }

        [HttpPost]
        public async Task<ActionResult> Create(AuthorDTO authorDTO)
        {
            var createdAuthor = await _service.AddAuthorAsync(authorDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdAuthor.Id }, createdAuthor);
        }
    }
}

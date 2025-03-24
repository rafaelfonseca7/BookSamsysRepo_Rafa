using Microsoft.AspNetCore.Mvc;
using BookSamsysAPI.Services;
using BookSamsysAPI.DTOs;

namespace BookSamsysAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookService _service;

        public BookController(BookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAll()
        {
            return Ok(await _service.GetAllBooksAsync());
        }

        [HttpGet("{isbn}")]
        public async Task<ActionResult<BookDTO>> GetByIsbn(string isbn)
        {
            var book = await _service.GetBookByIsbnAsync(isbn);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateBookDTO createBookDTO)
        {
            var createdBook = await _service.AddBookAsync(createBookDTO);
            return CreatedAtAction(nameof(GetByIsbn), new { isbn = createdBook.Isbn }, createdBook);
        }

        [HttpPut("{isbn}")]
        public async Task<ActionResult> Update(string isbn, CreateBookDTO updateBookDTO)
        {
            if (isbn != updateBookDTO.Isbn) return BadRequest();

            await _service.UpdateBookAsync(updateBookDTO);
            return NoContent();
        }

        [HttpDelete("{isbn}")]
        public async Task<ActionResult> Delete(string isbn)
        {
            await _service.DeleteBookAsync(isbn);
            return NoContent();
        }
    }
}
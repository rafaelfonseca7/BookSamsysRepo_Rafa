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

        [HttpGet("search/{title}")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetByTitle(string title)
        {
            var book = await _service.GetBookByTitleAsync(title);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateBookDTO createBookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 - Dados inválidos
            }

            try
            {
                var createdBook = await _service.AddBookAsync(createBookDTO);
                return CreatedAtAction(nameof(GetByIsbn), new { isbn = createdBook.Isbn }, createdBook);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 - Autor não encontrado
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("ISBN already exists"))
            {
                return Conflict(ex.Message); // 409 - ISBN duplicado
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred."); // Erros inesperados
            }
        }



        [HttpPut("{isbn}")]
        public async Task<ActionResult> Update(string isbn, CreateBookDTO updateBookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (isbn != updateBookDTO.Isbn)
            {
                return BadRequest("ISBN in the URL does not match ISBN in the body.");
            }

            var book = await _service.GetBookByIsbnAsync(isbn);
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            try
            {
                await _service.UpdateBookAsync(updateBookDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 - Autor não encontrado
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // 400 - Título inválido (validação manual)
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred."); // Erros inesperados
            }

        }


        [HttpDelete("{isbn}")]
        public async Task<ActionResult> Delete(string isbn)
        {
            await _service.DeleteBookAsync(isbn);
            return NoContent();
        }
    }
}
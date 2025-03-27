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
        // substiruir actionreuslt por messaging helper

        [HttpGet("isbn")]
        public async Task<ActionResult<BookDTO>> GetByIsbn([FromQuery] string isbn)
        {
            var book = await _service.GetBookByIsbnAsync(isbn);
            if (book == null) return NotFound();
            return Ok(book);
        }

        // / --» url query parameter
        [HttpGet("title")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetByTitle([FromQuery] string title)
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
                return StatusCode(500, "An unexpected error occurred.");
            }
        }



        [HttpPut("isbn")]
        public async Task<ActionResult> Update([FromQuery] string isbn, [FromBody] CreateBookDTO updateBookDTO)
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
                return StatusCode(500, "An unexpected error occurred.");
            }

        }


        [HttpDelete("isbn")]
        public async Task<ActionResult> Delete([FromQuery] string isbn)
        {
            await _service.DeleteBookAsync(isbn);
            return NoContent();
        }
        // livros de um autor, fe deve levar para uma nova pagina, com tabela dos livros do autor (esq ter info utor, e direita lita de livros
        // //dentro de cada row da tabela, ter um icone, q ao onclick, ir para detalhe do livro, nova rota, nova livro do detalhe do livro, pode ter um novo botao
        // // navegar detlhes autor, ou seja, full navegação)
        // criar  / ver o que sao .http files
    }
}
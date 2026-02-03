using Library_update.Abstracts;
using Library_update.DAL.Entities;
using Library_update.Models;
using Library_update.Models.Book;
using Microsoft.AspNetCore.Mvc;

namespace Library_update.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<List<BookDTO>>> GetAll(string? title, int? authorId)
        {
            return Ok(await _bookService.GetFilteredBooks(title, authorId));
        }

        [HttpGet("{id}/getbyid")]
        public async Task<ActionResult<BookDTO>> GetById(int id)
        {
            var book = await _bookService.GetById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _bookService.Save(request);
            return Ok();
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update(int id, [FromBody] BookDTO bookDTO)
        {
            await _bookService.Update(id, bookDTO);
            return Ok();
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.Delete(id);
            return NoContent();
        }
    }
}


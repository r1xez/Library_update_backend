using Library_update.Abstracts;
using Library_update.Models;
using Library_update.Models.Author;
using Microsoft.AspNetCore.Mvc;

namespace Library_update.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<List<AuthorDTO>>> GetAll()
        {
            return Ok(await _authorService.GetAll());
        }

        [HttpGet("{id}/getbyid")]
        public async Task<ActionResult<AuthorDTO>> GetById(int id)
        {
            var author = await _authorService.GetById(id);
            if (author == null) return NotFound();
            return Ok(author);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAuthorRequest request)
        {
            var newId = await _authorService.Save(request);
            return CreatedAtAction(nameof(GetById), new { id = newId }, request);
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update([FromBody] UpdateAuthorRequest request)
        {
            await _authorService.Update(request);
            return Ok();
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.Delete(id);
            return NoContent();
        }
    }
}

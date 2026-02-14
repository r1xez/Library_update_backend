using Library_update.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Library_update.Controllers
{
    [ApiController]
    [Route("api/gutendex")]
    public class GutendexController : ControllerBase
    {
        private readonly IGutendexService _gutendexService;

        public GutendexController(IGutendexService gutendexService)
        {
            _gutendexService = gutendexService;
        }

       
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks(string query)
        {
            var result = await _gutendexService.SearchBooksAsync(query);
            return Ok(result);
        }

        
        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularBooks()
        {
            var result = await _gutendexService.GetPopularBooksAsync();
            return Ok(result);
        }

       
        [HttpGet("allBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _gutendexService.GetAllBooksAsync();
            return Ok(result);
        }

        [HttpGet("allAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var result = await _gutendexService.GetAllAuthorsAsync();
            return Ok(result);
        }
        [HttpGet("images")]
        public async Task<IActionResult> GetImagesOfBooks()
        {
            var result = await _gutendexService.GetImagesOfBooks();
            return Ok(result);
        }


    }
}

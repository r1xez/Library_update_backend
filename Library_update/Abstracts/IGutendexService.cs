using System.Threading;
using System.Threading.Tasks;
using Library_update.Controllers;
using System.Collections.Generic;
using Library_update.Models.Gutendex;
using static Library_update.Models.Gutendex.GutendexDTO;

namespace Library_update.Abstracts
{
    public interface IGutendexService
    {
        Task<GutendexResponse> SearchBooksAsync(string query);
        Task<GutendexResponse> GetPopularBooksAsync();
        Task<GutendexResponse> GetAllBooksAsync();
        Task<List<GutendexAuthor>> GetAllAuthorsAsync();
        Task<List<BookImageDto>> GetImagesOfBooks();
    }
}
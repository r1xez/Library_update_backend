using Library_update.Abstracts;
using Library_update.Models.Gutendex; 
using System.Text.Json;
using static Library_update.Models.Gutendex.GutendexDTO; 

namespace Library_update.Services
{
    public class GutendexService : IGutendexService
    {
        private readonly HttpClient _httpClient;

        public GutendexService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GutendexResponse> SearchBooksAsync(string query)
        {
            var response = await _httpClient.GetAsync($"books?search={query}");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<GutendexResponse>(
                jsonString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return result;
        }

        public async Task<GutendexResponse> GetPopularBooksAsync()
        {
            var response = await _httpClient.GetAsync("books");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GutendexResponse>(
                jsonString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task<GutendexResponse> GetAllBooksAsync()
        {
            var response = await _httpClient.GetAsync("books");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GutendexResponse>(
                jsonString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
        public async Task<List<GutendexAuthor>> GetAllAuthorsAsync()
        {
            var response = await _httpClient.GetAsync("books");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<GutendexResponse>(
                jsonString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            var authors = result.Results
                .Where(b => b.Authors != null)
                .SelectMany(b => b.Authors)
                .GroupBy(a => a.Name)
                .Select(g => g.First())
                .ToList();

            return authors;
        }
        public async Task<List<BookImageDto>> GetImagesOfBooks()
        {
            var response = await _httpClient.GetAsync("books");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<GutendexResponse>(
                jsonString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            var images = result.Results
                .Select(b => new BookImageDto
                {
                    BookId = b.Id,
                    Title = b.Title,
                    ImageUrl = b.Formats?
                        .FirstOrDefault(f => f.Key.Contains("image"))
                        .Value
                })
                .Where(b => b.ImageUrl != null)
                .ToList();

            return images;
        }

        
    }
}
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Library_update.Models.Gutendex
{
    public class GutendexDTO
    {
        public class GutendexResponse
        {
            public int Count { get; set; }
            public string Next { get; set; }
            public List<GutendexBook> Results { get; set; }
        }
        public class GutendexBook
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public List<GutendexAuthor> Authors { get; set; }
            public List<string> Languages { get; set; }
            public Dictionary<string, string> Formats { get; set; }


        }
        public class BookImageDto
        {
            public int BookId { get; set; }
            public string Title { get; set; }
            public string ImageUrl { get; set; }
        }

        public class GutendexAuthor
        {
            public string Name { get; set; }

            [JsonPropertyName("birth_year")]
            public int? BirthYear { get; set; }

            [JsonPropertyName("death_year")]
            public int? DeathYear { get; set; }
        }
    }
}

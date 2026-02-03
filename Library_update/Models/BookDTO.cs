namespace Library_update.Models
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublishYear { get; set; }
        public decimal? Price { get; set; }
        public int AuthorId { get; set; }

        
        public AuthorDTO? Author { get; set; }
    }
}

using Library_update.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library_update.Models
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

       
        public ICollection<BookDTO> Books { get; set; } = new List<BookDTO>();
    }
}

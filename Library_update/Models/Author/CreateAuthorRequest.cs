using System.ComponentModel.DataAnnotations;

namespace Library_update.Models.Author
{
    public class CreateAuthorRequest
    {
        [Required, MinLength(2)]
        public string FirstName { get; set; }
        [Required, MinLength(2)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
}

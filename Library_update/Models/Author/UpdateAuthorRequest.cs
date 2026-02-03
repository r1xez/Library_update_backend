using System.ComponentModel.DataAnnotations;

namespace Library_update.Models.Author
{
    public class UpdateAuthorRequest
    {
        [Required]
        public int Id { get;  set; }

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        
        public DateTime BirthDate { get; set; }
       
    }
}

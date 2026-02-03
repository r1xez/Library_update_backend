using System.ComponentModel.DataAnnotations;

namespace Library_update.Models.Book
{
    public class UpdateBookRequest : IValidatableObject
    {

        

        [Required]
        [MinLength(2)]
        public string Title { get; set; }



        [Required]
        public int PublishYear { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int AuthorId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            
            var currentYear = DateTime.Now.Year;
            if (PublishYear < 1450 || PublishYear > currentYear)
            {
                results.Add(new ValidationResult(
                    $"Рік видання має бути між 1450 та {currentYear}.",
                    new[] { nameof(PublishYear) }));
            }
            return results;
        }
    }
}


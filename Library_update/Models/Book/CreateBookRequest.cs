using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Library_update.Models.Book
{
    public class CreateBookRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Title is required")]
        [MinLength(2, ErrorMessage = "Title must be at least 2 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Publish Year is required")]
        public int PublishYear { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid author")]
        public int AuthorId { get; set; }

       
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!string.IsNullOrEmpty(ISBN))
            {
                var isbnPattern = Regex.IsMatch(ISBN, @"^978-\d{10}$");
                if (!isbnPattern)
                {
                    results.Add(new ValidationResult(
                        "ISBN must match format 978-XXXXXXXXXX (13 digits starting with 978).",
                        new[] { nameof(ISBN) }));
                }
            }

            var currentYear = DateTime.Now.Year;
            if (PublishYear < 1450 || PublishYear > currentYear)
            {
                results.Add(new ValidationResult(
                    $"PublishYear must be between 1450 and {currentYear}.",
                    new[] { nameof(PublishYear) }));
            }
            return results;
        }
    }
}


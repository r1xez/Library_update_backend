using Library_update.Abstracts;
using Library_update.DAL.Abstracts;
using Library_update.DAL.Entities;
using Library_update.Models;
using Library_update.Models.Author;

namespace Library_update.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<AuthorDTO>> GetAll()
        {
            var authors = await _authorRepository.GetAllAsync();
            
            return authors.Select(MapToDTO).ToList();
        }

        public async Task<AuthorDTO> GetById(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return author == null ? null : MapToDTO(author);
        }

        public async Task<int> Save(CreateAuthorRequest request)
        {
            var author = new Author
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate
            };

            await _authorRepository.AddAsync(author);
            return author.Id;
        }

        public async Task Update(UpdateAuthorRequest request)
        {
            var author = await _authorRepository.GetByIdAsync(request.Id);
            if (author != null)
            {
                author.FirstName = request.FirstName;
                author.LastName = request.LastName;
                author.BirthDate = request.BirthDate;

                await _authorRepository.UpdateAsync(author);
            }
        }

        public async Task Delete(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
          
            if (author != null && author.Books != null && author.Books.Any())
            {
                throw new Exception("You cannot delete authors who have books.");
            }
            await _authorRepository.DeleteAsync(id);
        }

        private AuthorDTO MapToDTO(Author author)
        {
            return new AuthorDTO
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate,
                Books = author.Books?.Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    PublishYear = b.PublishYear,
                    Price = b.Price,
                    AuthorId = b.AuthorId
                }).ToList() ?? new List<BookDTO>()
            };
        }

    }
}



using Library_update.Abstracts;
using Library_update.DAL.Abstracts;
using Library_update.DAL.Entities;
using Library_update.Models;
using Library_update.Models.Book;

namespace Library_update.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

            public BookService(IBookRepository bookRepository)
            {
                _bookRepository = bookRepository;
            }

            public async Task<BookDTO> GetById(int id)
            {
                var book = await _bookRepository.GetByIdAsync(id);
                if (book == null) return null;

                return new BookDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    ISBN = book.ISBN,
                    PublishYear = book.PublishYear,
                    Price = book.Price,
                    AuthorId = book.AuthorId,
                    Author = book.Author == null ? null : new AuthorDTO
                    {
                        Id = book.Author.Id,
                        FirstName = book.Author.FirstName,
                        LastName = book.Author.LastName,
                        BirthDate = book.Author.BirthDate,
                        Books = new List<BookDTO>()
                    }
                };
            }

            
            public async Task Save(CreateBookRequest request)
            {
                
                var book = new Book
                {
                    Title = request.Title,
                    ISBN = request.ISBN,
                    PublishYear = request.PublishYear,
                    Price = request.Price,
                    AuthorId = request.AuthorId
                };

                await _bookRepository.AddAsync(book);
            }

            public async Task Update(int id, BookDTO bookDto)
            {
                var book = await _bookRepository.GetByIdAsync(id);
                if (book != null)
                {
                    book.Title = bookDto.Title;
                    book.PublishYear = bookDto.PublishYear;
                    book.Price = bookDto.Price;
                    book.AuthorId = bookDto.AuthorId;
                    await _bookRepository.UpdateAsync(book);
                }
            }

            public async Task Delete(int id)
            {
                await _bookRepository.DeleteAsync(id);
            }

            public async Task<List<BookDTO>> GetFilteredBooks(string title, int? authorId)
            {
                var books = await _bookRepository.GetAllAsync();

                if (!string.IsNullOrEmpty(title))
                    books = books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

                if (authorId.HasValue && authorId.Value > 0)
                    books = books.Where(b => b.AuthorId == authorId.Value).ToList();

                return books.Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    PublishYear = b.PublishYear,
                    Price = b.Price,
                    AuthorId = b.AuthorId,
                    Author = b.Author == null ? null : new AuthorDTO
                    {
                        Id = b.Author.Id,
                        FirstName = b.Author.FirstName,
                        LastName = b.Author.LastName,
                        BirthDate = b.Author.BirthDate,
                        Books = new List<BookDTO>() 
                    }
                }).ToList();
            }
        }
    }


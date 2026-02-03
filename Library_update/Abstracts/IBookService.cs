using Library_update.Models;
using Library_update.Models.Book;

namespace Library_update.Abstracts
{
    public interface IBookService
    {
        Task<BookDTO> GetById(int id);

       
        Task Save(CreateBookRequest request);

        Task Update(int id, BookDTO book);
        Task Delete(int id);
        Task<List<BookDTO>> GetFilteredBooks(string title, int? authorId);
    }
}

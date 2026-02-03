using Library_update.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_update.DAL.Abstracts
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<List<Book>> GetFilteredAsync(string titleFilter, int? authorId, string sortBy, bool desc);
        Task<Book> GetByIdAsync(int id);
        Task AddAsync(Book book);


        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
    }
}

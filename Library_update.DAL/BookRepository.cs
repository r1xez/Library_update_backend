using Library_update.DAL.Abstracts;
using Library_update.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_update.DAL
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .ToListAsync();
        }



        public async Task<Book> GetByIdAsync(int id)
        {

            return await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<Book>> GetFilteredAsync(string title, int? authorId, string sortBy, bool descending)
        {
            var query = _context.Books
                .Include(b => b.Author)
                .AsQueryable();


            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }


            if (authorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == authorId.Value);
            }


            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Year":
                        query = descending
                            ? query.OrderByDescending(b => b.PublishYear)
                            : query.OrderBy(b => b.PublishYear);
                        break;
                    default:
                        query = descending
                            ? query.OrderByDescending(b => b.Title)
                            : query.OrderBy(b => b.Title);
                        break;
                }
            }

            return await query.ToListAsync();
        }
    }
}

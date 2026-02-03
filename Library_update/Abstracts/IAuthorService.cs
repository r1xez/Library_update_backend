using Library_update.Models;
using Library_update.Models.Author;

namespace Library_update.Abstracts
{
    public interface IAuthorService
    {
        Task<List<AuthorDTO>> GetAll();
        Task<AuthorDTO?> GetById(int id);


        Task<int> Save(CreateAuthorRequest request);

        
        Task Update(UpdateAuthorRequest request);

        Task Delete(int id);
    }
}

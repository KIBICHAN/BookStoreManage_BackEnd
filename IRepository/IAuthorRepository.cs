using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository
{
    public interface IAuthorRepository
    {
        Task CreateAuthor(AuthorDTO authorDTO);

        Task EditAuthor(int authorID, AuthorDTO authorDTO);

        Task DeleteAuthor(int authorID);

        Task<List<Author>> getAllAuthor();

        Task<Author> getByID(int authorID);

        Task<List<Author>> getByName(string authorName);
    }
}

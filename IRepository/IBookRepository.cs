using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository
{
    public interface IBookRepository
    {
        Task CreateBook(BookDTO bookDTO);

        Task EditBook(int bookID, BookDTO books);

        Task DeleteBook(int bookID);

        Task<List<Book>> getAllBook();

        Task<Book> getByID(int bookID);

        Task<List<Book>> getByName(string bookName);
    }
}

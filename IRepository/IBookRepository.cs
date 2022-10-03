using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository
{
    public interface IBookRepository
    {
        void CreateBook(BookDTO book);

        void EditBook(int bookID, BookDTO books);

        void DeleteBook(int bookID);

        Task<List<Book>> getAllBook();

        Task<Book> getByID(int bookID);

        Task<List<Book>> getByName(string bookName);
    }
}

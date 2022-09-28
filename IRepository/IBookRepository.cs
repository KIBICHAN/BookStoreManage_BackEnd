using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository
{
    public interface IBookRepository
    {
        void CreateBook(string bookName, double price, int quantity, string image, string description, DateTime DateOfPublished, int fieldID, int publisherID, int authorID);

        void EditBook(int bookID, BookDTO books);

        void DeleteBook(int bookID);

        Task<List<Book>> getAllBook();

        Task<Book> getByID(int bookID);

        Task<List<Book>> getByName(string bookName);
    }
}

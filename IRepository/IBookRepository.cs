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
        Task<List<Book>> getByID(int bookID);
        Task<List<Book>> getByName(string bookName);
        Task<List<Book>> GetNewestBook();
        Task<List<BookDTO>> ImportExcel(IFormFile file);
        int totalNumberOfBook();
        int NumberOfSold();
        int NumberOfAcc();
        double NumberOfMoney();
<<<<<<< HEAD

        int NumberOfOrder();

        int NumberOfBookName();

=======
>>>>>>> f98f0f8bba0c36ad26bd9c66225becf53900f1f3
        //Task<List<Book>> getBookByField(int fieldID);
        Task<List<Book>> getSixBookBestSeller();
    }
}

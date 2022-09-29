using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreManage.Repository
{
    public class BookRepository : IBookRepository
    {
        public static Book book = new Book();
        private BookManageContext _context;

        public BookRepository(BookManageContext context)
        {
            _context = context;
        }

        public void CreateBook(string bookName, double price, int quantity, string image, string description, DateTime dateOfPublished, int fieldID, int publisherID, int authorID)
        {
            book = new Book();
            book.BookName = bookName;
            book.Price = price;
            book.Quantity = quantity;
            book.Image = image;
            book.Description = description;
            book.DateOfPublished = dateOfPublished;
            book.FieldID = fieldID;
            book.PublisherID = publisherID;
            book.AuthorID = authorID;

            _context.Books.Add(book);
            _context.SaveChanges();

        }

        public void DeleteBook(int BookID)
        {
            var tmp = _context.Books.Find(BookID);
            if(tmp != null)
            {
                _context.Books.Remove(tmp);
                _context.SaveChanges(); 
            }    
        }

        public void EditBook(int bookID, BookDTO bookDTO)
        {
            var tmp = _context.Books.Find(bookID);
            if( tmp != null)
            {
                tmp.BookName = bookDTO.bookName;
                tmp.Price = bookDTO.price;
                tmp.Quantity = bookDTO.quantity;
                tmp.Image = bookDTO.image;
                tmp.Description = bookDTO.description;
                tmp.DateOfPublished = bookDTO.DateOfPublished;
                tmp.FieldID = bookDTO.fieldID;
                tmp.PublisherID = bookDTO.publisherID;
                tmp.AuthorID = bookDTO.authorID;

                _context.Update(tmp);
                _context.SaveChangesAsync();
            }
        }

        public async Task<List<Book>> getAllBook()
        {
            var book = await _context.Books.ToListAsync();
            return book;
        }

        public async Task<Book> getByID(int idBook)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == idBook);
            return book;
        }

        public async Task<List<Book>> getByName(string bookName)
        {
            var field = await _context.Books.Where(b => b.BookName.Contains(bookName)).ToListAsync();
            //ToListAsync();
            return field;
        }
    }
}

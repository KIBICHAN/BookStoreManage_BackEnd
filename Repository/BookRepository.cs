#nullable disable
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

        public async Task CreateBook(BookDTO bookDTO)
        {
            book = new Book();
            book.BookName = bookDTO.bookName;
            book.Price = bookDTO.price;
            book.Quantity = bookDTO.quantity;
            book.Image = bookDTO.image;
            book.Description = bookDTO.description;
            book.DateOfPublished = bookDTO.DateOfPublished;
            book.FieldID = bookDTO.fieldID;
            book.PublisherID = bookDTO.publisherID;
            book.AuthorID = bookDTO.authorID;

            _context.Books.Add(book);
            await _context.SaveChangesAsync();


        }

        public async Task DeleteBook(int BookID)
        {
            var tmp = _context.Books.Find(BookID);
            if(tmp != null)
            {
                _context.Books.Remove(tmp);
                await _context.SaveChangesAsync(); 
            }    
        }

        public async Task EditBook(int bookID, BookDTO bookDTO)
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
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Book>> getAllBook()
        {
            var book = await _context.Books.Include(b => b.Author)
                                           .Include(b => b.Publisher)
                                           .Include(b => b.Field)
                                           .ToListAsync();
            return book;
        }

        public async Task<Book> getByID(int idBook)
        {
            var book = await _context.Books.Include(b => b.Author)
                                           .Include(b => b.Publisher)
                                           .Include(b => b.Field)
                                           .FirstOrDefaultAsync(b => b.BookID == idBook);
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

#nullable disable
using System.Net;
using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

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
            if (tmp != null)
            {
                _context.Books.Remove(tmp);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditBook(int bookID, BookDTO bookDTO)
        {
            var tmp = _context.Books.Find(bookID);
            if (tmp != null)
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
            var book = await _context.Books.ToListAsync();
            return book;
        }

        /*public async Task<List<Book>> getBookByField(int fieldID)
        {
            //var book = await _context.Books.Where<b => b.>
        }*/

        public async Task<Book> getByID(int idBook)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == idBook);
            return book;
        }

        public async Task<List<Book>> getByName(string bookName)
        {
            var books = await _context.Books.Where(b => b.BookName.Contains(bookName)).ToListAsync();
            //ToListAsync();
            return books;
        }

        public async Task<List<BookDTO>> ImportExcel(IFormFile file)
        {
            try
            {
                var list = new List<BookDTO>();
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowcount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowcount; row++)
                        {
                            string fieldName = worksheet.Cells[row, 6].Value.ToString();
                            int fieldId = _context.Fields.Where(f => f.FieldName.Trim().Contains(fieldName.Trim())).Select(f => f.FieldID).FirstOrDefault();
                            string authorName = worksheet.Cells[row, 7].Value.ToString();
                            int authorId = _context.Authors.Where(a => a.AuthorName.Trim().Contains(authorName.Trim())).Select(a => a.AuthorID).FirstOrDefault();
                            string publisherName = worksheet.Cells[row, 8].Value.ToString();
                            int publisherId = _context.Publishers.Where(p => p.PublisherName.Trim().Contains(publisherName.Trim())).Select(p => p.PublisherID).FirstOrDefault();

                            list.Add(new BookDTO
                            {
                                bookName = worksheet.Cells[row, 1].Value.ToString(),
                                price = double.Parse(worksheet.Cells[row, 2].Value.ToString()),
                                quantity = Int32.Parse(worksheet.Cells[row, 3].Value.ToString()),
                                image = worksheet.Cells[row, 4].Value.ToString(),
                                description = worksheet.Cells[row, 5].Value.ToString(),
                                fieldID = fieldId,
                                publisherID = publisherId,
                                authorID = authorId,
                                DateOfPublished = DateTime.Parse(worksheet.Cells[row, 9].Value.ToString())
                            });
                        }
                    }
                }
                return list;
            }catch(Exception ex){
                throw new BadHttpRequestException(ex.Message);
            }
        }
    }
}

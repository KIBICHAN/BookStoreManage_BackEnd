#nullable disable
using System.Collections;
using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
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
            book.StripeID = bookDTO.stripeId;
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
            var check = _context.OrderDetails.Where(b => b.BookID == BookID).FirstOrDefault();
            if (check != null)
            {
                throw new BadHttpRequestException("Có thằng nào đó order rồi, xóa sao mà được");
            }
            _context.Books.Remove(tmp);
            await _context.SaveChangesAsync();
        }



        public async Task EditBook(int bookID, BookDTO bookDTO)
        {
            var tmp = _context.Books.Find(bookID);
            if (tmp != null)
            {
                tmp.StripeID = bookDTO.stripeId;
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
            var book = await _context.Books.Include(b => b.OrderDetails).ToListAsync();
            return book;
        }

        public async Task<List<Book>> getByName(string bookName)
        {
            var books = await _context.Books.Where(b => b.BookName.Contains(bookName)).ToListAsync();
            //ToListAsync();
            return books;
        }

        public async Task<List<Book>> getByID(int idBook)
        {
            var books = await _context.Books.Include(b => b.Author).Include(b => b.Publisher).Include(b => b.Field).Where(b => b.BookID == idBook).Select(b => new Book
            {
                StripeID = b.StripeID,
                BookID = b.BookID,
                BookName = b.BookName,
                Price = b.Price,
                Quantity = b.Quantity,
                Image = b.Image,
                Description = b.Description,
                DateOfPublished = b.DateOfPublished,
                Field = b.Field,
                Author = b.Author,
                Publisher = b.Publisher
            }).ToListAsync();
            return books;
        }

        public async Task<List<Book>> GetNewestBook()
        {
            DateTime currentDate = DateTime.UtcNow.Date;
            DateTime oneWeek = DateTime.UtcNow.Date.AddDays(-30);

            var books = await _context.Books.Include(b => b.Author).Include(b => b.Publisher)
            .Where(b => b.DateOfPublished.CompareTo(currentDate) < 0 && b.DateOfPublished.CompareTo(oneWeek) > 0).Select(b => new Book
            {
                StripeID = b.StripeID,
                BookID = b.BookID,
                BookName = b.BookName,
                Price = b.Price,
                Quantity = b.Quantity,
                Image = b.Image,
                Description = b.Description,
                DateOfPublished = b.DateOfPublished,
                Author = b.Author,
                Publisher = b.Publisher
            }).ToListAsync();
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
                            try
                            {
                                string fieldName, authorName, publisherName;

                                fieldName = worksheet.Cells[row, 6].Value == null ? "" : worksheet.Cells[row, 6].Value.ToString();
                                int fieldId = _context.Fields.Where(f => f.FieldName.Trim() == fieldName.Trim()).Select(f => f.FieldID).FirstOrDefault();
                                authorName = worksheet.Cells[row, 7].Value == null ? "" : worksheet.Cells[row, 7].Value.ToString();
                                int authorId = _context.Authors.Where(a => a.AuthorName.Trim() == authorName.Trim()).Select(a => a.AuthorID).FirstOrDefault();
                                publisherName = worksheet.Cells[row, 8].Value == null ? "" : worksheet.Cells[row, 8].Value.ToString();
                                int publisherId = _context.Publishers.Where(p => p.PublisherName.Trim() == publisherName.Trim()).Select(p => p.PublisherID).FirstOrDefault();
                                
                                if (fieldId != 0 && authorId != 0 && publisherId != 0)
                                {
                                    list.Add(new BookDTO
                                    {
                                        bookName = worksheet.Cells[row, 1].Value == null ? "" : worksheet.Cells[row, 1].Value.ToString(),
                                        price = double.Parse(worksheet.Cells[row, 2].Value == null ? "0" : worksheet.Cells[row, 2].Value.ToString()),
                                        quantity = Int32.Parse(worksheet.Cells[row, 3].Value == null ? "0" : worksheet.Cells[row, 3].Value.ToString()),
                                        image = worksheet.Cells[row, 4].Value  == null ? "" : worksheet.Cells[row, 4].Value.ToString(),
                                        description = worksheet.Cells[row, 5].Value == null ? "" : worksheet.Cells[row, 5].Value.ToString(),
                                        fieldID = fieldId,
                                        publisherID = publisherId,
                                        authorID = authorId,
                                        DateOfPublished = DateTime.Parse(worksheet.Cells[row, 9].Value.ToString()),
                                        stripeId = worksheet.Cells[row, 10].Value.ToString()
                                    });
                                }else{
                                    continue;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Loi o day ne: " + row, e.Message);
                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
        }

        public int totalNumberOfBook()
        {
            int count = _context.Books.Sum(b => b.Quantity);
            return count;
        }

        public int NumberOfSold()
        {
            int count = _context.OrderDetails.Sum(od => od.Quantity);
            return count;
        }

        public int NumberOfAcc()
        {
            int count = _context.Accounts.Count(b => b.RoleID == 2);
            return count;
        }

        public double NumberOfMoney()
        {
            double count = _context.Orders.Sum(or => or.TotalAmount);
            return count;
        }

        public int NumberOfOrder()
        {
            int count = _context.Orders.Count();
            return count;
        }

        public int NumberOfBookName()
        {
            int count = _context.Books.Count();
            return count;

        }


        public async Task<ArrayList> sumquantity()
        {
            //ArrayList<SumDTO> list = new ArrayList<SumDTO>;

            var list = new ArrayList();
            SumDTO aoMaThat = new SumDTO();

            var getIDBook = await _context.OrderDetails.Select(or => or.BookID).Distinct().ToListAsync();
            for (int j = 0; j < getIDBook.Count(); j++)
            {
                var check = await _context.OrderDetails.Where(or => or.BookID.Equals(getIDBook[j])).ToListAsync();

                int tmp = 0;
                int sum = 0;
                int bookID = 0;
                for (int i = 0; i < check.Count(); i++)
                {
                    bookID = check[i].BookID;
                    tmp = check[i].Quantity;
                    sum += tmp;

                    aoMaThat = new SumDTO();
                    aoMaThat.bookID = bookID;
                    aoMaThat.quantity = sum;
                }
                list.Add(aoMaThat);
            }
            return list;
        }

        public async Task<List<Book>> getSixBookBestSeller()
        {
            var rankBooks = await sumquantity();
            List<Book> list = new List<Book>();
            List<SumDTO> array = rankBooks.OfType<SumDTO>().ToList();

            array.Sort((a, b) => b.quantity.CompareTo(a.quantity));

            for (int i = 0; i < 6; i++)
            {
                var aoMaThat = await _context.Books.Include(b => b.Author).Where(b => b.BookID == array[i].bookID).FirstOrDefaultAsync();

                list.Add(aoMaThat);
            }

            return list;
        }
    }
}

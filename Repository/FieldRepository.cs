#nullable disable
using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStoreManage.Repository
{
    public class FieldRepository : IFieldRepository
    {
        private static Field field = new Field();
        private static Random random = new Random();
        private BookManageContext _context;

        public FieldRepository(BookManageContext context)
        {
            _context = context;
        }
        public async Task CreateField(FieldDTO _field)
        {
            field = new Field();
            field.FieldName = _field.FieldName;
            field.FieldDescription = _field.FieldDescription;

            _context.Add(field);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteField(int idField)
        {
            var tmp = _context.Fields.Find(idField);
            if (tmp != null)
            {
                _context.Remove(tmp);
                await _context.SaveChangesAsync();
            }

        }

        public async Task EditField(int idField, FieldDTO fields)
        {
            var tmp = _context.Fields.Find(idField);
            if (tmp != null)
            {
                tmp.FieldName = fields.FieldName;
                tmp.FieldDescription = fields.FieldDescription;
                _context.Update(tmp);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Field>> getAllField()
        {
            var field = await _context.Fields.Include(f => f.Books).ToListAsync();
            return field;
        }

        public async Task<List<Field>> getByID(int idField)
        {
            List<Book> book = _context.Books.Where(b => b.FieldID == idField)
            .Join(_context.Authors, b => b.AuthorID, a => a.AuthorID, (b, a) => new { b, a })
            .Select(b => new Book
            {
                BookID = b.b.BookID,
                StripeID = b.b.StripeID,
                BookName = b.b.BookName,
                Price = b.b.Price,
                Quantity = b.b.Quantity,
                Image = b.b.Image,
                Description = b.b.Description,
                DateOfPublished = b.b.DateOfPublished,
                Author = new Author
                {
                    AuthorID = b.a.AuthorID,
                    AuthorName = b.a.AuthorName
                }
            }).ToList();

            var field = await _context.Fields.Where(f => f.FieldID == idField)
            .Select(f => new Field
            {
                FieldID = f.FieldID,
                FieldName = f.FieldName,
                FieldDescription = f.FieldDescription,
                Books = book
            }).ToListAsync();

            return field;
        }

        public IEnumerable<Field> GetEightRows()
        {
            int count = countField();

            IEnumerable<Field> field = _context.Fields.OfType<Field>().Include(f => f.Books).ToList().Skip(random.Next(1, count) - 8).Take(8);
            return field;
        }

        public IEnumerable<Field> GetTwoRows()
        {
            int count = countField();

            var field = _context.Fields.Select(f => new Field
            {
                FieldID = f.FieldID,
                FieldName = f.FieldName,
                FieldDescription = f.FieldDescription,
                Books = _context.Books.Where(b => b.FieldID == f.FieldID)
                    .Join(_context.Authors, b => b.AuthorID, a => a.AuthorID, (b, a) => new { b, a })
                    .Select(b => new Book
                    {
                        BookID = b.b.BookID,
                        StripeID = b.b.StripeID,
                        BookName = b.b.BookName,
                        Price = b.b.Price,
                        Quantity = b.b.Quantity,
                        Image = b.b.Image,
                        Description = b.b.Description,
                        DateOfPublished = b.b.DateOfPublished,
                        Author = new Author
                        {
                            AuthorID = b.a.AuthorID,
                            AuthorName = b.a.AuthorName
                        }
                    }).ToList()
            }).ToList().Skip(random.Next(1, count) - 2).Take(2);

            return field;
        }

        public async Task<List<Field>> getByName(string fieldName)
        {
            var field = await _context.Fields.Where(f => f.FieldName.Contains(fieldName)).ToListAsync();
            //ToListAsync();
            return field;
        }

        public int countField()
        {
            int count = _context.Fields.Count();
            return count;
        }
    }
}

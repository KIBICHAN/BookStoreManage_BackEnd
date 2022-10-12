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
            field.FieldName = _field.name;
            field.FieldDescription = _field.description;

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
                tmp.FieldName = fields.name;
                tmp.FieldDescription = fields.description;
                _context.Update(tmp);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Field>> getAllField()
        {
            var field = await _context.Fields.Include(f => f.Books).ToListAsync();
            return field;
        }

        public async Task<Field> getByIDTest(int idField)
        {
            var field = await _context.Fields.Include(f => f.Books).FirstOrDefaultAsync(f => f.FieldID == idField);
            return field;
        }

        public async Task<List<FieldBook>> getByID(int idField)
        {
            List<FieldBook> _field = new List<FieldBook>();
            List<string> authorName = new List<string>();
            List<string> publisherName = new List<string>();
            var field = await _context.Fields.Include(f => f.Books).Where(f => f.FieldID == idField).ToListAsync();
            for (int i = 0; i < field.Count(); i++)
            {
                var id = field[i].Books.Where(b => b.FieldID == field[i].FieldID).Select(b => new
                {
                    b.BookName,
                    b.AuthorID,
                    b.Description,
                    b.PublisherID,
                    b.Price,
                    b.Image,
                    b.DateOfPublished
                }).ToList();

                for (int y = 0; y < id.Count; y++)
                {
                    authorName.Add(_context.Authors.Where(a => a.AuthorID == id[y].AuthorID).Select(a => a.AuthorName).FirstOrDefault());
                    publisherName.Add(_context.Publishers.Where(p => p.PublisherID == id[y].PublisherID).Select(p => p.PublisherName).FirstOrDefault());
                    _field.Add(new FieldBook
                    {
                        fieldId = field[i].FieldID,
                        bookName = id[y].BookName,
                        bookDescription = id[y].Description,
                        price = id[y].Price,
                        image = id[y].Image,
                        dateofpublished = id[y].DateOfPublished,
                        fiedlName = field[i].FieldName,
                        authorName = authorName[y],
                        publisherName = publisherName[y],

                    });
                }
            }
            return _field;
        }

        public IEnumerable<Field> getFiveRows()
        {
            int count = countField();

            IEnumerable<Field> field = _context.Fields.OfType<Field>().ToList().Skip(random.Next(1, count) - 5).Take(5);
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

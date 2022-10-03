#nullable disable
using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreManage.Repository
{
    public class FieldRepository : IFieldRepository
    {
        public static Field field = new Field();
        private BookManageContext _context;

        public FieldRepository(BookManageContext context)
        {
            _context = context;
        }
        public void CreateField(FieldDTO _field)
        {
            field = new Field();
            field.FieldName = _field.Name;
            field.FieldDescription = _field.description;
            _context.Add(field);
            _context.SaveChanges();
        }

        public void DeleteField(int idField)
        {
            var tmp = _context.Fields.Find(idField);
            if(tmp != null)
            {
                _context.Remove(tmp);
                _context.SaveChanges();
            }
            
        }

        public void EditField(int idField ,FieldDTO fields)
        {
            var tmp = _context.Fields.Find(idField);
            if (tmp != null)
            {
                tmp.FieldName = fields.Name;
                tmp.FieldDescription = fields.description;
                _context.Update(tmp);
                _context.SaveChangesAsync();
            }
        }

        public async Task<List<Field>> getAllField()
        {
            var field = await _context.Fields.ToListAsync();
            return field;
        }

        public async Task<Field> getByID(int idField)
        {
            var field = await _context.Fields.FirstOrDefaultAsync(f => f.FieldID == idField);
            return field;
        }

        public async Task<List<Field>> getByName(string fieldName)
        {
            var field = await _context.Fields.Where(f => f.FieldName == fieldName).ToListAsync();
                //ToListAsync();
            return field;

        }

    }
}

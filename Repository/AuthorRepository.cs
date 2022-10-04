#nullable disable
using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreManage.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private Author author;
        private readonly BookManageContext _context;

        public AuthorRepository(BookManageContext context)
        {
            _context = context;
        }

        public async Task CreateAuthor(AuthorDTO authorDTO)
        {
            author = new Author();

            author.AuthorName = authorDTO.AuthorName;

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthor(int authorID)
        {
            var author = _context.Authors.FirstOrDefault(a => a.AuthorID == authorID);
            _context.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task EditAuthor(int authorID, AuthorDTO authorDTO)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorID == authorID);

            author.AuthorName = authorDTO.AuthorName;
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Author>> getAllAuthor()
        {
            var list = await _context.Authors.ToListAsync();
            return list;
        }

        public async Task<Author> getByID(int authorID)
        {
            var list = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorID == authorID);
            return list;
        }

        public async Task<List<Author>> getByName(string authorName)
        {
            var list = await _context.Authors.Where(a => a.AuthorName.Contains(authorName)).ToListAsync();
            return list;
        }
    }
}

#nullable disable
using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreManage.Repository;

public class PublisherRepository : IPublisherRepository
{
    private Publisher publisher;
    private readonly BookManageContext _context;
    public PublisherRepository(BookManageContext context)
    {
        _context = context;
    }

    public async Task<List<Publisher>> GetAll()
    {

        var list = await _context.Publishers.Include(p => p.Books).ToListAsync();
        return list;

    }

    public async Task<List<Publisher>> GetName(string name)
    {

        var list = await _context.Publishers.Where(p => p.PublisherName.Trim().ToLower().Contains(name.Trim().ToLower())).ToListAsync();
        return list;

    }

    public async Task<Publisher> FindByID(int id)
    {

        var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.PublisherID == id);
        return publisher;

    }

    public async Task CreateNew(PublisherDto _publisher)
    {

        publisher = new Publisher();

        publisher.PublisherName = _publisher.PublisherName;
        publisher.FieldAddress = _publisher.FieldAddress;

        _context.Publishers.Add(publisher);
        await _context.SaveChangesAsync();

    }

    public async Task EditPublisher(int id, PublisherDto _publisher)
    {

        var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.PublisherID == id);

        publisher.PublisherName = _publisher.PublisherName;
        publisher.FieldAddress = _publisher.FieldAddress;

        _context.Publishers.Update(publisher);
        await _context.SaveChangesAsync();

    }

    public async Task DeletePublisher(int id)
    {

        var publisher = _context.Publishers.FirstOrDefault(p => p.PublisherID == id);
        _context.Remove(publisher);
        await _context.SaveChangesAsync();
    }
}
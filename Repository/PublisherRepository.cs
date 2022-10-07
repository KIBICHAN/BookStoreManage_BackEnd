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
        try
        {
            var list = await _context.Publishers.ToListAsync();
            return list;
        }
        catch (Exception e)
        {
            Console.WriteLine("Fail!", e);
        }
        return null;
    }

    public async Task<List<Publisher>> GetName(string name)
    {
        try
        {
            var list = await _context.Publishers.Where(p => p.PublisherName.Contains(name)).ToListAsync();
            return list;
        }
        catch (Exception e)
        {
            Console.WriteLine("Fail!", e);
        }
        return null;
    }

    public async Task<Publisher> FindByID(int id)
    {
        try
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.PublisherID == id);
            return publisher;
        }
        catch (Exception e)
        {
            Console.WriteLine("Fail!", e);
        }
        return null;
    }

    public async Task CreateNew(PublisherDto _publisher)
    {
        try
        {
            publisher = new Publisher();

            publisher.PublisherName = _publisher.PublisherName;
            publisher.FieldAddress = _publisher.FieldAddress;

            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fail!", e);
        }
    }

    public async Task EditPublisher(int id, PublisherDto _publisher)
    {
        try
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.PublisherID == id);

            publisher.PublisherName = _publisher.PublisherName;
            publisher.FieldAddress = _publisher.FieldAddress;

            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fail!", e);
        }
    }

    public async Task DeletePublisher(int id)
    {
        try
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.PublisherID == id);
            _context.Remove(publisher);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fail!", e);
        }
    }
}
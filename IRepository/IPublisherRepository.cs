using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository;

public interface IPublisherRepository {
    Task<List<Publisher>> GetAll();
    Task<List<Publisher>> GetName(string name);
    Task<Publisher> FindByID(int id);
    Task CreateNew(PublisherDto _publisher);
    Task EditPublisher(int id, PublisherDto _publisher);
    Task DeletePublisher(int id);
}
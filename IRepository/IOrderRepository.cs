using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository;

public interface IOrderRepository {
    Task<List<Order>> GetAll();
    Task<Order> FindByOrderID(int id);
    Task<OrderDetail> FindByOrderDetailID(int id);
    Task<int> CreateNewOrder(int accountId);
    Task UpdateStatus(int id, bool status);
    Task CreateNewOrderDetail(List<OrderDetailDto> _list);
    Task DeleteOrder(int id);
}
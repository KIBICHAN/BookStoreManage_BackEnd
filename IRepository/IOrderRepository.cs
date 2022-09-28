using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository;

public interface IOrderRepository {
    Task<List<Order>> GetAll();
    Task<Order> FindByOrderID(int id);
    Task<OrderDetail> FindByOrderDetailID(int id);
    Task CreateNewOrder(OrderDto _order);
    Task UpdateStatus(int id, double status);
    Task CreateNewOrderDetail(OrderDetailDto _detail);
    Task UpdateTotalPrice(int id, double quantity);
    Task DeleteOrderDetail(int id);
}
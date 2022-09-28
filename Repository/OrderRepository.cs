#nullable disable
using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreManage.Repository;

public class OrderRepository : IOrderRepository{
    private Order order;
    private OrderDetail detail;
    private readonly BookManageContext _context;
    public OrderRepository(BookManageContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAll()
    {
        var orderList = await _context.Orders.ToListAsync();
        return orderList;
    }

    public async Task<Order> FindByOrderID(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == id);
        return order;
    }

    public async Task<OrderDetail> FindByOrderDetailID(int id)
    {
        var detail = await _context.OrderDetails.FirstOrDefaultAsync(o => o.OrderDetailID == id);
        return detail;
    }

    public async Task CreateNewOrder(OrderDto _order){
        order = new Order();

        order.OrderStatus = _order.OrderStatus;
        order.DateOfOrder = DateTime.Today;
        order.AccountID = _order.AccountID;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStatus(int id, double status){
        var order = await FindByOrderID(id);

        order.OrderStatus = status;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task CreateNewOrderDetail(OrderDetailDto _detail){
        detail = new OrderDetail();
        
        detail.OrderID = _detail.OrderID;
        detail.BookID = _detail.BookID;
        detail.Quantity = _detail.Quantity;
        detail.Price = _detail.Price;
        detail.TotalPrice = _detail.Quantity * _detail.Price;

        _context.OrderDetails.Add(detail);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTotalPrice(int id, double quantity){
        var detail = await FindByOrderDetailID(id);

        detail.TotalPrice = quantity * detail.Price;

        _context.OrderDetails.Update(detail);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderDetail(int id){
        var detail = await FindByOrderDetailID(id);
        _context.Remove(detail);
        await _context.SaveChangesAsync();
    }
}
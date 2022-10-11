#nullable disable
using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreManage.Repository;

public class OrderRepository : IOrderRepository
{
    private Order order;
    private OrderDetail detail;
    private readonly BookManageContext _context;
    public OrderRepository(BookManageContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAll()
    {
        var orderList = await _context.Orders.Include(o => o.OrderDetails).ToListAsync();
        return orderList;
    }

    public async Task<Order> FindByOrderID(int id)
    {
        var order = await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.OrderID == id);
        return order;
    }

    public async Task<OrderDetail> FindByOrderDetailID(int id)
    {
        var detail = await _context.OrderDetails.FirstOrDefaultAsync(o => o.OrderDetailID == id);
        return detail;
    }

    public async Task CreateNewOrder(OrderDto _order)
    {
        order = new Order();

        order.OrderStatus = _order.OrderStatus;
        order.DateOfOrder = DateTime.Today;
        order.AccountID = _order.AccountID;
        order.TotalAmount = 0;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStatus(int id, int status)
    {
        var order = await FindByOrderID(id);

        order.OrderStatus = status;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task CreateNewOrderDetail(List<OrderDetailDto> _list, int orederId)
    {
        double total = 0;
        var _order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == orederId);

        for (int i = 0; i < _list.Count; i++)
        {
            detail = new OrderDetail();
            var _book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == _list[i].BookID);

            detail.OrderID = orederId;
            detail.BookID = _list[i].BookID;
            detail.Quantity = _list[i].Quantity;
            detail.Price = _book.Price;
            detail.TotalPrice = _list[i].Quantity * _book.Price;

            _book.Quantity = _book.Quantity - _list[i].Quantity;

            total = total + detail.TotalPrice;

            _context.OrderDetails.Add(detail);
            _context.Books.Update(_book);
        }

        _order.TotalAmount = total;
        _context.Orders.Update(_order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTotalPrice(int id, int quantity)
    {
        var detail = await FindByOrderDetailID(id);

        detail.TotalPrice = quantity * detail.Price;
        detail.Quantity = quantity;

        _context.OrderDetails.Update(detail);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderDetail(int id)
    {
        var detail = await FindByOrderDetailID(id);
        _context.Remove(detail);
        await _context.SaveChangesAsync();
    }

}
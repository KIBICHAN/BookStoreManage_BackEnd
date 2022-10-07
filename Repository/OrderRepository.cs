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
        try
        {
            var orderList = await _context.Orders.Include(o => o.OrderDetails).ToListAsync();
            return orderList;
        }
        catch (Exception e)
        {
            Console.WriteLine("Fail!", e);
        }
        return null;
    }

    public async Task<Order> FindByOrderID(int id)
    {
        try
        {
            var order = await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.OrderID == id);
            return order;
        }
        catch (Exception e)
        {
            Console.WriteLine("Fail!", e);
        }
        return null;
    }

    public async Task<OrderDetail> FindByOrderDetailID(int id)
    {
        try
        {
            var detail = await _context.OrderDetails.FirstOrDefaultAsync(o => o.OrderDetailID == id);
            return detail;
        }
        catch (Exception e)
        {
            Console.WriteLine("Fail!", e);
        }
        return null;
    }

    public async Task CreateNewOrder(OrderDto _order)
    {
        try
        {
            order = new Order();

            order.OrderStatus = _order.OrderStatus;
            order.DateOfOrder = DateTime.Today;
            order.AccountID = _order.AccountID;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fail!", e);
        }
    }

    public async Task UpdateStatus(int id, double status)
    {
        try
        {
            var order = await FindByOrderID(id);

            order.OrderStatus = status;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fail!", e);
        }
    }

    public async Task CreateNewOrderDetail(OrderDetailDto _detail)
    {
        try
        {
            detail = new OrderDetail();

            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == _detail.BookID);

            detail.OrderID = _detail.OrderID;
            detail.BookID = _detail.BookID;
            detail.Quantity = _detail.Quantity;
            detail.Price = book.Price;
            detail.TotalPrice = _detail.Quantity * book.Price;

            _context.OrderDetails.Add(detail);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fail!", e);
        }
    }

    public async Task UpdateTotalPrice(int id, double quantity)
    {
        try
        {
            var detail = await FindByOrderDetailID(id);

            detail.TotalPrice = quantity * detail.Price;
            detail.Quantity = quantity;

            _context.OrderDetails.Update(detail);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fail!", e);
        }
    }

    public async Task DeleteOrderDetail(int id)
    {
        try
        {
            var detail = await FindByOrderDetailID(id);
            _context.Remove(detail);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Fail!", e);
        }
    }
}
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

    public async Task<List<Order>> FindByOrderID(int id)
    {
        var order = await _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Book).Where(o => o.OrderID == id).ToListAsync();
        return order;
    }


    public async Task<List<Order>> FindByCustomerID(int id)
    {
        var customer = await _context.Orders.Include(o => o.OrderDetails).Where(o => o.AccountID == id).ToListAsync();
        return customer;
    }

    public async Task<OrderDetail> FindByOrderDetailID(int id)
    {
        var detail = await _context.OrderDetails.Include(b => b.Book).FirstOrDefaultAsync(o => o.OrderDetailID == id);
        return detail;
    }

    public async Task<int> CreateNewOrder(int accountId)
    {
        order = new Order();

        order.OrderStatus = true;
        order.DateOfOrder = DateTime.Today;
        order.AccountID = accountId;
        order.TotalAmount = 0;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return _context.Orders.OrderByDescending(a => a.OrderID).First().OrderID;
    }

    public async Task UpdateStatus(int id, ChangeStatusDto status)
    {
        if (status.Status == false)
        {
            var order = await _context.Orders.FindAsync(id);
            order.OrderStatus = status.Status;

            var _orderDetail = await _context.OrderDetails.Where(d => d.OrderID == id).Select(d => new OrderDetailDto
            {
                BookID = d.BookID,
                Quantity = d.Quantity
            }).ToListAsync();

            for (int i = 0; i < _orderDetail.Count; i++)
            {
                var _book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == _orderDetail[i].BookID);

                _book.Quantity = _book.Quantity + _orderDetail[i].Quantity;
                _context.Books.Update(_book);
            }

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }

    public async Task CreateNewOrderDetail(List<OrderDetailDto> _list)
    {
        double total = 0;

        try
        {
            for (int i = 0; i < _list.Count; i++)
            {
                var _order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == _list[i].OrderID);
                var _book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == _list[i].BookID);

                detail = new OrderDetail();

                detail.OrderID = _list[i].OrderID;
                detail.BookID = _list[i].BookID;
                detail.Quantity = _list[i].Quantity;
                detail.Price = _book.Price;
                detail.TotalPrice = _list[i].Quantity * _book.Price;

                _book.Quantity = _book.Quantity - _list[i].Quantity;

                total = total + detail.TotalPrice;

                _context.OrderDetails.Add(detail);
                _context.Books.Update(_book);
                _order.TotalAmount = total;
                _context.Orders.Update(_order);

                await _context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task DeleteOrder(int id)
    {
        var order = await _context.Orders.Where(o => o.OrderID == id).FirstOrDefaultAsync();
        if (order.OrderStatus == false)
        {
            _context.Remove(order);
            await _context.SaveChangesAsync();
        }else{
            throw new BadHttpRequestException("Cant delete this Order because it in current delivery!");
        }
    }
}
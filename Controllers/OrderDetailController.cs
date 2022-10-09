using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManage.Controllers;

[Route("[controller]")]
[ApiController]
public class OrderDetailController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    public OrderDetailController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet("GetByOrderDetailId/{id}")]
    public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
    {
        var detail = await _orderRepository.FindByOrderDetailID(id);
        return Ok(detail);
    }

    [HttpPost("Create")]
    public async Task<ActionResult> CreateNew(List<OrderDetailDto> list, int orederId)
    {
        await _orderRepository.CreateNewOrderDetail(list, orederId);
        return Ok();
    }

    [HttpPut("UpdateTotal/{id}")]
    public async Task<ActionResult> UpdateSubTotal(int id, int quantity)
    {
        await _orderRepository.UpdateTotalPrice(id, quantity);
        return Ok();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Remove(int id)
    {
        await _orderRepository.DeleteOrderDetail(id);
        return Ok();
    }
}
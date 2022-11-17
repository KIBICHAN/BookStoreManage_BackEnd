using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManage.Controllers;

[Route("[controller]")]
[ApiController]
//[Authorize(Roles = "Customer,Admin,Staff")]
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
        try
        {
            var detail = await _orderRepository.FindByOrderDetailID(id);
            return Ok(detail);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Create")]
    public async Task<ActionResult> CreateNew(List<OrderDetailDto> list)
    {
        try
        {
            await _orderRepository.CreateNewOrderDetail(list);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
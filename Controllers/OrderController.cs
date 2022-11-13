using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManage.Controllers;

[Route("[controller]")]
[Authorize(Roles = "Customer,Admin,Staff")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet("Get")]
    public async Task<ActionResult<List<Order>>> GetAll()
    {
        try
        {
            var list = await _orderRepository.GetAll();
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetByOrderId/{id}")]
    public async Task<ActionResult<List<Order>>> GetOrder(int id)
    {
        try
        {
            var order = await _orderRepository.FindByOrderID(id);
            return Ok(order);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetByCustomerId/{id}")]
    public async Task<ActionResult<List<Order>>> GetOrderByCustomer(int id)
    {
        try
        {
            var order = await _orderRepository.FindByCustomerID(id);
            return Ok(order);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Create/{accountId}")]
    public async Task<ActionResult> CreateNew(int accountId)
    {
        try
        {
            int orderId = await _orderRepository.CreateNewOrder(accountId);
            return Ok(orderId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("UpdateStatus/{id}")]
    public async Task<ActionResult> UpdateStatus(int id, bool status)
    {
        try
        {
            await _orderRepository.UpdateStatus(id, status);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        try
        {
            await _orderRepository.DeleteOrder(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
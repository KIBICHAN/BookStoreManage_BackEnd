using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManage.Controllers;

    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository){
            _orderRepository = orderRepository;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<Order>>> GetAll(){
            var list = await _orderRepository.GetAll();
            return Ok(list);
        }

        [HttpGet("GetByOrderId/{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id){
            var order = await _orderRepository.FindByOrderID(id);
            return Ok(order);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateNew(OrderDto order){
            await _orderRepository.CreateNewOrder(order);
            return Ok();
        }

        [HttpPut("UpdateStatus/{id}")]
        public async Task<ActionResult> UpdateStatus(int id, int status){
            await _orderRepository.UpdateStatus(id, status);
            return Ok();
        }
    }
using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManage.Controllers;

[Route("[controller]")]
[ApiController]
public class PublisherController : ControllerBase
{
    private readonly IPublisherRepository _publisherRepository;
    public PublisherController(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    [HttpGet("Get")]
    public async Task<ActionResult<List<Publisher>>> GetAll()
    {
        var list = await _publisherRepository.GetAll();
        return Ok(list);
    }

    [HttpGet("GetByName/{name}")]
    public async Task<ActionResult<List<Publisher>>> GetName(string name)
    {
        var list = await _publisherRepository.GetName(name);
        return Ok(list);
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<Publisher>> GetId(int id)
    {
        var publisher = await _publisherRepository.FindByID(id);
        return Ok(publisher);
    }

    [HttpPost("Create")]
    public async Task<ActionResult> CreatNew(PublisherDto publisher)
    {
        await _publisherRepository.CreateNew(publisher);
        return Ok(publisher);
    }

    [HttpPut("Update/{id}")]
    public async Task<ActionResult> EditPublisher(int id, PublisherDto publisher)
    {
        await _publisherRepository.EditPublisher(id, publisher);
        return Ok();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> DeletePublisher(int id)
    {
        await _publisherRepository.DeletePublisher(id);
        return Ok();
    }
}
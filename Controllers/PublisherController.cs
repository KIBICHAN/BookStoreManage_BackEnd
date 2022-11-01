using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManage.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Staff")]
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
        try
        {
            var list = await _publisherRepository.GetAll();
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetByName/{name}")]
    public async Task<ActionResult<List<Publisher>>> GetName(string name)
    {
        try
        {
            var list = await _publisherRepository.GetName(name);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<Publisher>> GetId(int id)
    {
        try
        {
            var publisher = await _publisherRepository.FindByID(id);
            return Ok(publisher);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Create")]
    public async Task<ActionResult> CreatNew(PublisherDto publisher)
    {
        try
        {
            await _publisherRepository.CreateNew(publisher);
            return Ok(publisher);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("Update/{id}")]
    public async Task<ActionResult> EditPublisher(int id, PublisherDto publisher)
    {
        try
        {
            await _publisherRepository.EditPublisher(id, publisher);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> DeletePublisher(int id)
    {
        try
        {
            await _publisherRepository.DeletePublisher(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
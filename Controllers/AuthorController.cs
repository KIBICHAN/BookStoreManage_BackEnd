using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManage.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin"), Authorize(Roles = "Staff")]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _repository;

        public AuthorController(IAuthorRepository Authorrepository)
        {
            _repository = Authorrepository;
        }


        [HttpGet("Get")]
        public async Task<ActionResult<List<Author>>> GetAll()
        {
            try
            {
                var list = await _repository.getAllAuthor();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<List<Author>>> GetName(string name)
        {
            try
            {
                var list = await _repository.getByName(name);
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Author>> GetId(int id)
        {
            try
            {
                var author = await _repository.getByID(id);
                return Ok(author);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(AuthorDTO authorDTO)
        {
            try
            {
                await _repository.CreateAuthor(authorDTO);
                return Ok(authorDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> EditPublisher(int id, AuthorDTO authorDTO)
        {
            try
            {
                await _repository.EditAuthor(id, authorDTO);
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
                await _repository.DeleteAuthor(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

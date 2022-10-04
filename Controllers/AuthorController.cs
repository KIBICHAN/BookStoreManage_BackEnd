using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManage.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
            var list = await _repository.getAllAuthor();
            return Ok(list);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<List<Author>>> GetName(string name)
        {
            var list = await _repository.getByName(name);
            return Ok(list);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Author>> GetId(int id)
        {
            var author = await _repository.getByID(id);
            return Ok(author);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(AuthorDTO authorDTO)
        {
            await _repository.CreateAuthor(authorDTO);
            return Ok(authorDTO);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> EditPublisher(int id, AuthorDTO authorDTO)
        {
            await _repository.EditAuthor(id, authorDTO);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeletePublisher(int id)
        {
            await _repository.DeleteAuthor(id);
            return Ok();
        }
    }
}

using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreManage.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly BookManageContext _context;

        public BookController(BookManageContext context, IBookRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<Book>>> GetAlls()
        {
            var list = await _repository.getAllBook();
            return Ok(list);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Book>> GetByID(int id)
        {
            var result = await _repository.getByID(id);
            return Ok(result);
        }


        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<List<Book>>> GetByName(string name)
        {
            var result = await _repository.getByName(name);
            return Ok(result);
        }

 
        [HttpPost("Create")]
        public async Task<ActionResult> Post(BookDTO bookDTO)
        {
            await _repository.CreateBook(bookDTO);
            //return Ok(_repository.ShowLastOfList);
            return Ok(_context.Fields.ToList());
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Put(int id, BookDTO bookDTO)
        {
            await _repository.EditBook(id, bookDTO);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteBook(id);
            return Ok(_repository);
        }
    }
}

using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreManage.Controllers
{
    [Route("api/[controller]")]
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

        // GET: api/<BookController>
        [HttpGet("Get")]
        public IActionResult GetAlls()
        {
            return Ok(_context.Books.ToList());
        }

        // GET api/<BookController>/5
        [HttpGet("GetById/{id}")]
        public IActionResult GetByID(int id)
        {
            var result = _repository.getByID(id);
            if (id == null)
            {
                return NotFound();
            }
            return Ok(result.Result);
        }


        // GET api/<BookController>/Truyện Conan
        [HttpGet("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var result = _repository.getByName(name);
            if (name != null)
            {
                return Ok(result.Result);
            }
            return NotFound();
        }

        // POST api/<BookController>
        [HttpPost("Create")]
        public IActionResult Post(string bookName, double price, int quantity, string image, string description, DateTime DateOfPublished, int fieldID, int publisherID, int authorID)
        {
            _repository.CreateBook(bookName, price, quantity, image, description, DateOfPublished, fieldID, publisherID, authorID);
            //return Ok(_repository.ShowLastOfList);
            return Ok(_context.Fields.ToList());
        }

        // PUT api/<BookController>/5
        [HttpPut("Update/{id}")]
        public IActionResult Put(int id, BookDTO bookDTO)
        {
            _repository.EditBook(id, bookDTO);
            if (id == null)
            {
                return NotFound();
            }
            return Ok();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteBook(id);
            if (id == null)
            {
                return NotFound();
            }

            return Ok(_repository);
        }
    }
}

using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

        [AllowAnonymous]
        [HttpGet("Get")]
        public async Task<ActionResult<List<Book>>> GetAlls()
        {
            try
            {
                var list = await _repository.getAllBook();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<List<Book>>> GetByID(int id)
        {
            try
            {
                var result = await _repository.getByID(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<List<Book>>> GetByName(string name)
        {
            try
            {
                var result = await _repository.getByName(name);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetNewestBook")]
        public async Task<ActionResult<List<Book>>> GetNewestBook()
        {
            try
            {
                var list = await _repository.GetNewestBook();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("SixBookBestSeller")]
        public async Task<ActionResult> getSixBookBestSeller()
        {
            try
            {
                var result = await _repository.getSixBookBestSeller();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[Authorize(Roles = "Admin,Staff")]
        [HttpGet("TotalOfBook")]
        public ActionResult NumberOfBook()
        {
            try
            {
                int count = _repository.totalNumberOfBook();
                return Ok(count);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("NumberOfSold")]
        public ActionResult NumberOfSold()
        {
            try
            {
                int count = _repository.NumberOfSold();
                return Ok(count);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("NumberOfAcc")]
        public ActionResult NumberOfSAcc()
        {
            try
            {
                int count = _repository.NumberOfAcc();
                return Ok(count);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[Authorize(Roles = "Admin,Staff")]
        [HttpGet("NumberOfMoney")]
        public ActionResult NumberOfMoney()
        {
            try
            {
                double count = _repository.NumberOfMoney();
                return Ok(count);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("NumberOfOrder")]
        public ActionResult NumberOfOrder()
        {
            try
            {
                int count = _repository.NumberOfOrder();
                return Ok(count);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("NumberOfBookName")]
        public ActionResult NumberOfBookName()
        {
            try
            {
                int count = _repository.NumberOfBookName();
                return Ok(count);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpPost("Create")]
        public async Task<ActionResult> CreateNewBook(BookDTO bookDTO)
        {
            try
            {
                await _repository.CreateBook(bookDTO);
                //return Ok(_repository.ShowLastOfList);
                return Ok(_context.Books.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpPost("Import")]
        public async Task<ActionResult<List<Book>>> ImportFile(IFormFile file)
        {
            try
            {
                var list = await _repository.ImportExcel(file);
                for (int i = 0; i < list.Count; i++)
                {
                    var listname = await _repository.getByName(list[i].bookName);
                    if (listname.Count == 0)
                    {
                        await _repository.CreateBook(list[i]);
                    }
                    //return StatusCode(StatusCodes.Status500InternalServerError, "Can't insert Book Name: " + list[i].bookName);
                }
                return Ok(_context.Books.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateBook(int id, BookDTO bookDTO)
        {
            try
            {
                await _repository.EditBook(id, bookDTO);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            try
            {
                await _repository.DeleteBook(id);
                return Ok(_repository);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        

    }
}

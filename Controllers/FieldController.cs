using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreManage.Controllers
{
    [Route("[controller]")]
    [ApiController]
    // [AllowAnonymous]
    public class FieldController : ControllerBase
    {
        private readonly IFieldRepository _repository;
        private readonly BookManageContext _context;

        public FieldController(BookManageContext context, IFieldRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: api/<FieldController>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Field>>> GetAlls()
        {
            try
            {
                var list = await _repository.getAllField();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Get5Row")]
        public ActionResult<List<Field>> GetFiveRows()
        {
            try
            {
                var list = _repository.getFiveRows();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<FieldController>/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<List<Field>>> GetByID(int id)
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

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<List<Field>>> GetName(string name)
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

        // POST api/<FieldController>
        [HttpPost]
        public async Task<ActionResult> Post(FieldDTO field)
        {
            try
            {
                await _repository.CreateField(field);
                //return Ok(_repository.ShowLastOfList);
                return Ok(_context.Fields.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<FieldController>/5
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Put(int id, FieldDTO fields)
        {
            try
            {
                await _repository.EditField(id, fields);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<FieldController>/5
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _repository.DeleteField(id);
                return Ok(_repository);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

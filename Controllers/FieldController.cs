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
        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet("GetEightRow")]
        public ActionResult<List<Field>> GetEightRows()
        {
            try
            {
                var list = _repository.GetEightRows();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetTwoRow")]
        public ActionResult<List<Field>> GetTwoRows()
        {
            try
            {
                var list = _repository.GetTwoRows();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<FieldController>/5
        [AllowAnonymous]
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

        [AllowAnonymous]
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
        [Authorize(Roles = "Admin,Staff")]
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
        [Authorize(Roles = "Admin,Staff")]
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
        [Authorize(Roles = "Admin,Staff")]
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

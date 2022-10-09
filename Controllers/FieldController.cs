using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
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
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Field>>> GetAlls()
        {
            var list = await _repository.getAllField();
            return Ok(list);
        }

        // GET api/<FieldController>/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Field>> GetByID(int id)
        {
            var result = await _repository.getByID(id);
            return Ok(result);
        }
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<List<Field>>> GetName(string name)
        {
            var list = await _repository.getByName(name);
            return Ok(list);
        }

        // POST api/<FieldController>
        [HttpPost]
        public async Task<ActionResult> Post(FieldDTO field)
        {
            await _repository.CreateField(field);
            //return Ok(_repository.ShowLastOfList);
            return Ok(_context.Fields.ToList());
        }

        // PUT api/<FieldController>/5
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Put(int id, FieldDTO fields)
        {
            await _repository.EditField(id, fields);
            return Ok();
        }

        // DELETE api/<FieldController>/5
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteField(id);
            return Ok(_repository);
        }
    }
}

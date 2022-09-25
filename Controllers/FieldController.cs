using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreManage.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet]
        public IActionResult GetAlls()
        {
            return Ok(_context.Fields.ToList());
        }

        // GET api/<FieldController>/5
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var result = _repository.getByID(id);
            if(id == null)
            {
                return NotFound();
            }
            return Ok(result.Result);
        }

        // POST api/<FieldController>
        [HttpPost]
        public IActionResult Post(string name, string description)
        {
            _repository.CreateField(name, description);
            //return Ok(_repository.ShowLastOfList);
            return Ok(_context.Fields.ToList());
        }

        // PUT api/<FieldController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, FieldDTO fields)
        {
            _repository.EditField(id, fields);
            if(id == null)
            {
                return NotFound();
            }
            return Ok();
        }

        // DELETE api/<FieldController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteField(id);
            if(id == null)
            {
                return NotFound();
            }

            return Ok(_repository);
        }
    }
}

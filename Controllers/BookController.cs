﻿using BookStoreManage.DTO;
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

        // GET: api/<BookController>
        [HttpGet]
        public IActionResult GetAlls()
        {
            return Ok(_context.Books.ToList());
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var result = _repository.getByID(id);
            return Ok(result.Result);
        }


        // GET api/<BookController>/Truyện Conan
        [HttpGet("{name}")]
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
        [HttpPost]
        public IActionResult Post(BookDTO book)
        {
            _repository.CreateBook(book);
            //return Ok(_repository.ShowLastOfList);
            return Ok(_context.Books.ToList());
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, BookDTO bookDTO)
        {
            _repository.EditBook(id, bookDTO);
            return Ok();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteBook(id);
            return Ok(_repository);
        }
    }
}

using Library_Manager.Application.Services;
using Library_Manager.Core.Entities;
using Library_Manager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Models.ModelsBook;


namespace Library_Manager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        private readonly IBookService _service;
        public BooksController(LibraryDbContext context, IBookService service) 
        { 
            _context = context;
            _service = service;
        }

        //api/livros?quey=net core
        [HttpGet]
        public IActionResult Get(string query)
        {
            var result = _service.GetAllBooks(query);

            if(!result.IsSuccess)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetBookById(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateBookModels createBook)
        {
            var result = _service.CreateBook(createBook);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateBookModel updateBook)
        {
            if (updateBook == null)
            {
                return BadRequest();
            }

            var result = _service.UpdateBook(id, updateBook);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.DeleteBook(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }

            return NoContent();
        }
    }
}

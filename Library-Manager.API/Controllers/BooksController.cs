using Library_Manager.Application.Models;
using Library_Manager.Application.Services;
using Library_Manager.Core.Entities;
using Library_Manager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;


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
            if (createBook == null)
            {
                return BadRequest("O modelo do livro não pode ser nulo.");
            }
            var result = _service.CreateBook(createBook);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateBookModels createBook)
        {
            if (createBook == null)
            {
                return BadRequest();
            }

            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = createBook.Title;
            book.ISBN = createBook.ISBN;
            book.Year = createBook.YearOfPublication;
            book.AuthorId = createBook.AuthorId;

            _context.Books.Update(book);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

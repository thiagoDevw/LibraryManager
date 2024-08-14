using Library_Manager.Application.Models;
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
        public BooksController(LibraryDbContext context) 
        { 
            _context = context;
        }

        //api/livros?quey=net core
        [HttpGet]
        public IActionResult Get(string query)
        {
            var books = _context.Books 
                .Where(b => b.Title.Contains(query) || b.ISBN.Contains(query))
                .ToList();

            if (!books.Any())
            {
                return NotFound();
            }

            return Ok(books);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateBookModels createBook)
        {
            if (createBook == null)
            {
                return BadRequest();
            }

            var book = new Book
            {
                Title = createBook.Title,
                ISBN = createBook.ISBN,
                Year = createBook.YearOfPublication,
                AuthorId = createBook.AuthorId
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
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

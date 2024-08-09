using Library_Manager.API.Models;
using Microsoft.AspNetCore.Mvc;


namespace Library_Manager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //api/livros?quey=net core
        [HttpGet]
        public IActionResult Get(string query)
        {
            return Ok();
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateBookModels createBook)
        {
            if (createBook == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = createBook.Id }, createBook);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateBookModels createBook)
        {
            if (createBook == null)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}

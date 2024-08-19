using Library_Manager.Core.Entities;
using Library_Manager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Models.ModelsUsers;

namespace Library_Manager.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        public UsersController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound(); // Retorna 404 se o usuário não for encontrado
            }

            return Ok(user); // Retorna 200 com o usuário encontrado
        }

        //POST: api/users
        [HttpPost]
        public IActionResult PostUsers([FromBody] CreateUserModels model)
        {
            if (model == null)
            {
                return BadRequest(); // Retorna 400 se o modelo for nulo
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, [FromBody] CreateUserModels model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = model.Name;
            user.Email = model.Email;


            _context.Users.Update(user);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NoContent();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

using Library_Manager.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Manager.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PostUsers(CreateUserModels model)
        {
            return Ok();
        }
    }
}

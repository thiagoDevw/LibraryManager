using Library_Manager.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Models.ModelsLoan;

namespace Library_Manager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoansService _loanService;
        public LoanController(ILoansService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet("{id}")]
        public IActionResult GetLoanById( int id)
        {
            var result = _loanService.GetLoanById(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }


        [HttpPost]
        public IActionResult PostLoan([FromBody] CreateLoanModel model)
        {
            var result = _loanService.CreateLoan(model);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPut("return/{id}")]
        public IActionResult ReturnBook(int id, [FromBody] DateTime returnDate)
        {
            var result = _loanService.ReturnBook(id, returnDate);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLoan(int id)
        {
            var result = _loanService.DeleteLoan(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}

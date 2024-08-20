using Library_Manager.Core.Entities;
using Library_Manager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.ModelsLoan;

namespace Library_Manager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        public LoanController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetLoanById( int id)
        {
            var loan = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .AsNoTracking()
                .FirstOrDefault(l => l.Id == id);

            if (loan == null)
            {
                return NotFound("Empréstimo não encontrado. ");
            }

            var loanDto = new LoanDto
            {
                Id = loan.Id,
                UserId = loan.User.Id,
                UserName = loan.User.Name,
                BookId = loan.BookId,
                BookTitle = loan.Book.Title,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate
            };

            return Ok(loanDto);

        }


        [HttpPost]
        public IActionResult PostLoan([FromBody] CreateLoanModel model)
        {
            if (model == null)
            {
                return BadRequest("Dados Inválidos");
            }

            // Verifica se o livro existe
            var book = _context.Books.Find(model.BookId);
            if (book == null)
            {
                return NotFound("Livro não encontrado");
            }

            // Verifica se o usuário existe
            var user = _context.Users.Find(model.UserId);
            if (user == null)
            {
                return NotFound("Usuário não existe");
            }

            // Cria o novo empréstimo
            var loan = new Loan
            {
                UserId = model.UserId,
                BookId = model.BookId,
                LoanDate = model.LoanDate
            };

            _context.Loans.Add(loan);
            _context.SaveChanges();

            var loanDto = new LoanDto
            {
                Id = loan.Id,
                UserId = loan.UserId,
                UserName = user.Name,
                BookId = loan.BookId,
                BookTitle = book.Title,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate
            };

            return CreatedAtAction(nameof(GetLoanById), new { id = loan.Id }, loanDto);
        }

        [HttpPut("return/{id}")]
        public IActionResult ReturnBook(int id, [FromBody] DateTime returnDate)
        {
            var loan = _context.Loans.Find(id);
            if (loan == null)
            {
                return NotFound("Empréstimo não encontrado. ");
            }

            if (returnDate < loan.LoanDate)
            {
                return BadRequest("A data de devolução não pode ser anterior à data do empréstimo.");
            }

            loan.ReturnDate = returnDate;
            _context.SaveChanges();

            var loanPeriod = 5;
            var dueDate = loan.LoanDate.AddDays(loanPeriod);
            var delay = (returnDate - dueDate).Days;

            if (delay > 0)
            {
                return Ok($"Livro devolvido com {delay} dias de atraso.");
            }
            else
            {
                return Ok("Livro devolvido no prazo.");
            } 
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLoan(int id)
        {
            var loan = _context.Loans.Find(id);
            if (loan == null)
            {
                return NotFound();
            }

            _context.Loans.Remove(loan);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

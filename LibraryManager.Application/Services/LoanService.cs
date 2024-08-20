using Library_Manager.Application.Models;
using Library_Manager.Core.Entities;
using Library_Manager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.ModelsLoan;

namespace Library_Manager.Application.Services
{
    public class LoanService : ILoansService
    {
        private readonly LibraryDbContext _context;
        public LoanService(LibraryDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<LoanDto> CreateLoan(CreateLoanModel model)
        {
            if (model == null)
            {
                return ResultViewModel<LoanDto>.Error("Dados Inválidos");
            }

            // Verifica se o livro existe
            var book = _context.Books.Find(model.BookId);
            if (book == null)
            {
                return ResultViewModel<LoanDto>.Error("Livro não encontrado");
            }

            // Verifica se o usuário existe
            var user = _context.Users.Find(model.UserId);
            if (user == null)
            {
                return ResultViewModel<LoanDto>.Error("Usuário não existe");
            }

            // Verifica se o livro já está emprestado
            bool isBookLoaned = _context.Loans.Any(loan => loan.BookId == model.BookId && loan.ReturnDate == null);
            if (isBookLoaned)
            {
                return ResultViewModel<LoanDto>.Error("Este livro já está emprestado. ");
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
                UserId = loan.UserId,
                UserName = user.Name,
                BookId = loan.BookId,
                BookTitle = book.Title,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate
            };

            return ResultViewModel<LoanDto>.Success(loanDto);
        }

        public ResultViewModel DeleteLoan(int id)
        {
            var loan = _context.Loans.Find(id);
            if (loan == null)
            {
                return ResultViewModel.Error("Empréstimo não encontrado.");
            }

            _context.Loans.Remove(loan);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel<LoanDto> GetLoanById(int id)
        {
            var loan = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .AsNoTracking()
                .FirstOrDefault(l => l.Id == id);

            if (loan == null)
            {
                return ResultViewModel<LoanDto>.Error("Empréstimo não encontrado. ");
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

            return ResultViewModel<LoanDto>.Success(loanDto);
        }

        public ResultViewModel<string> ReturnBook(int id, DateTime returnDate)
        {
            var loan = _context.Loans.Find(id);
            if (loan == null)
            {
                return ResultViewModel<string>.Error("Empréstimo não encontrado.");
            }

            if (returnDate < loan.LoanDate)
            {
                return ResultViewModel<string>.Error("A data de devolução não pode ser anterior à data do empréstimo.");
            }

            loan.ReturnDate = returnDate;
            _context.SaveChanges();

            var loanPeriod = 5;
            var dueDate = loan.LoanDate.AddDays(loanPeriod);
            var delay = (returnDate - dueDate).Days;

            if (delay > 0)
            {
                return ResultViewModel<string>.Success($"Livro devolvido com {delay} dias de atraso.");
            }
            else
            {
                return ResultViewModel<string>.Success("Livro devolvido no prazo.");
            }
        }
    }
}

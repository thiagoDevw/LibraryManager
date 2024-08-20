using Library_Manager.Application.Models;
using Library_Manager.Core.Entities;
using Library_Manager.Infrastructure.Persistence;
using Models.ModelsLoan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Manager.Application.Services
{
    public interface ILoansService
    {
        ResultViewModel<LoanDto> GetLoanById(int id);
        ResultViewModel<LoanDto> CreateLoan(CreateLoanModel model);
        ResultViewModel<string> ReturnBook(int id, DateTime returnDate);
        ResultViewModel DeleteLoan(int id);
    }

    public class LoanService : ILoansService
    {
        private readonly LibraryDbContext _context;
        public LoanService(LibraryDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<LoanDto> CreateLoan(CreateLoanModel model)
        {
            throw new NotImplementedException();
        }

        public ResultViewModel DeleteLoan(int id)
        {
            throw new NotImplementedException();
        }

        public ResultViewModel<LoanDto> GetLoanById(int id)
        {
            throw new NotImplementedException();
        }

        public ResultViewModel<string> ReturnBook(int id, DateTime returnDate)
        {
            throw new NotImplementedException();
        }
    }
}

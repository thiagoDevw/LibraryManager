using Library_Manager.Application.Models;
using Library_Manager.Core.Entities;
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
}

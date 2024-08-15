using Library_Manager.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Manager.Application.Services
{
    internal interface IBookService
    {
        Task<ResultViewModel> CreateBookAsync(CreateBookModels models);
        Task<ResultViewModel> UpdateBookAsync(int id, UpdateBookModel model);
        Task<ResultViewModel<BookDetailsModel>> GetBookByIdAsync(int id);
    }
}

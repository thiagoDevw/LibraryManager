using Library_Manager.Application.Models;
using Library_Manager.Core.Entities;
using Models.ModelsBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Library_Manager.Application.Services
{
    public interface IBookService
    {
        ResultViewModel<Book> CreateBook(CreateBookModels models);
        ResultViewModel UpdateBook(int id, UpdateBookModel model);
        ResultViewModel<BookDetailsModel> GetBookById(int id);
        ResultViewModel<IEnumerable<BookViewModel>> GetAllBooks(string query);
        ResultViewModel DeleteBook(int id);
    }
}

using Library_Manager.Application.Models;
using Library_Manager.Core.Entities;
using Library_Manager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Library_Manager.Application.Services
{
    internal interface IBookService
    {
        Task<ResultViewModel> CreateBookAsync(CreateBookModels models);
        Task<ResultViewModel> UpdateBookAsync(int id, UpdateBookModel model);
        Task<ResultViewModel<BookDetailsModel>> GetBookByIdAsync(int id);
        Task<ResultViewModel<IEnumerable<BookViewModel>>> GetAllBooksAsync(string query);
        Task<ResultViewModel> DeleteBookAsync(int id);
    }

    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;
        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> CreateBookAsync(CreateBookModels models)
        {
            if (models == null)
            {
                return ResultViewModel.Error("Modelo inválido.");
            }

            var book = new Book
            {
                Title = models.Title,
                ISBN = models.ISBN,
                Year = models.YearOfPublication,
                AuthorId = models.AuthorId
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            return ResultViewModel.Success("Livro criado com sucesso.");
        }

        public Task<ResultViewModel> DeleteBookAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultViewModel> UpdateBookAsync(int id, UpdateBookModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultViewModel<IEnumerable<BookViewModel>>> GetAllBooksAsync(string query)
        {
            var books = await _context.Books
                .Where(b => b.Title.Contains(query) || b.ISBN.Contains(query))
                .ToListAsync();

            if (!books.Any())
            {
                return ResultViewModel<IEnumerable<BookViewModel>>.NotFound("Nenhum livro encontrado.");
            }

            var bookViewModels = books.Select(b => new BookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                // Adicione outros campos conforme necessário
            }).ToList();

            return ResultViewModel<IEnumerable<BookViewModel>>.Success(bookViewModels);
        }

        Task<ResultViewModel<BookDetailsModel>> IBookService.GetBookByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

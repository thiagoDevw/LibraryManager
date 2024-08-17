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
        ResultViewModel CreateBook(CreateBookModels models);
        ResultViewModel UpdateBook(int id, UpdateBookModel model);
        ResultViewModel<BookDetailsModel> GetBookById(int id);
        ResultViewModel<IEnumerable<BookViewModel>> GetAllBooks(string query);
        ResultViewModel DeleteBook(int id);
    }

    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;
        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public ResultViewModel CreateBook(CreateBookModels models)
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

        public ResultViewModel DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return ResultViewModel.NotFound("Livro não encontrado");
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            return ResultViewModel.Success("Livro removido com sucesso.");
        }

        public ResultViewModel UpdateBook(int id, UpdateBookModel model)
        {
            if (model == null)
            {
                return ResultViewModel.Error("Modelo inválido");
            }

            var book = _context.Books.Find(id);
            if (book == null)
            {
                return ResultViewModel.NotFound("Livro não encontrado.");
            }

            book.Title = model.Title;
            book.ISBN = model.ISBN;
            book.Year = model.YearOfPublication;
            book.AuthorId = model.AuthorId;

            _context.Books.Update(book);
            _context.SaveChanges();

            return ResultViewModel.Success("Livro atualizado com sucesso.");
        }

        public  ResultViewModel<IEnumerable<BookViewModel>> GetAllBooks(string query)
        {
            var books = _context.Books
                .Where(b => b.Title.Contains(query) || b.ISBN.Contains(query))
                .ToList();

            if (!books.Any())
            {
                return ResultViewModel<IEnumerable<BookViewModel>>.NotFound("Nenhum livro encontrado.");
            }

            var bookViewModels = books.Select(b => new BookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
            }).ToList();

            return ResultViewModel<IEnumerable<BookViewModel>>.Success(bookViewModels);
        }

        ResultViewModel<BookDetailsModel> IBookService.GetBookById(int id)
        {
            var book = _context.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return ResultViewModel<BookDetailsModel>.NotFound("Livro não encontrado");
            }

            var bookDetails = new BookDetailsModel
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                Year = book.Year,
                AuthorId = book.AuthorId,
            };

            return ResultViewModel<BookDetailsModel>.Success(bookDetails);
        }
    }
}

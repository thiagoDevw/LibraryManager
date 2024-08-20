using Library_Manager.Application.Models;
using Library_Manager.Core.Entities;
using Library_Manager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.ModelsBook;

namespace Library_Manager.Application.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;
        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<IEnumerable<BookViewModel>> GetAllBooks(string query)
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
                Title = book.Title,
                ISBN = book.ISBN,
                Year = book.Year,
                AuthorId = book.AuthorId,
            };

            return ResultViewModel<BookDetailsModel>.Success(bookDetails);
        }
        public ResultViewModel<Book> CreateBook(CreateBookModels models)
        {
            if (models == null)
            {
                return ResultViewModel<Book>.Error("Modelo inválido.");
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

            return ResultViewModel<Book>.Success(book);
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

    }
}

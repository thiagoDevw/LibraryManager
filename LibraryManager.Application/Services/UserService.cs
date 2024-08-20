using Library_Manager.Application.Models;
using Library_Manager.Core.Entities;
using Library_Manager.Infrastructure.Persistence;
using Models.ModelsUsers;

namespace Library_Manager.Application.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryDbContext _context;
        public UserService(LibraryDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<User> CreateUser(CreateUserModels model)
        {
            if (model == null)
            {
                return ResultViewModel<User>.Error("Modelo inválido.");
            }

            if (EmailExists(model.Email))
            {
                return ResultViewModel<User>.Error("O email já está em uso.");
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return ResultViewModel<User>.Success(user);
        }

        public ResultViewModel DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return ResultViewModel.Error("Usuário não encontrado.");
            }

            //Verificar se o usuário tem empréstimos ativos
            bool hasLoans = _context.Loans.Any(loan => loan.UserId == id);
            if (hasLoans)
            {
                return ResultViewModel.Error("Não é possivel deletar o usuário, pois ele possui empréstimos ativos. ");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return ResultViewModel.Success("Usuário deletado com sucesso.");
        }

        public bool EmailExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return _context.Users.Any(u => u.Email == email);
        }

        public ResultViewModel<UserDetailsModel> GetUserById(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return ResultViewModel<UserDetailsModel>.Error("Usuário não encontrado."); 
            }

            var userDetails = new UserDetailsModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };

            return ResultViewModel<UserDetailsModel>.Success(userDetails); 
        }

        public ResultViewModel UpdateUser(int id, UpdateUserModel model)
        {
            if (model == null)
            {
                return ResultViewModel.Error("Modelo inválido");
            }

            var user = _context.Users.Find(id);
            if (user == null)
            {
                return ResultViewModel.Error("Usuário não encontrado."); ;
            }

            user.Name = model.Name;
            user.Email = model.Email;


            _context.Users.Update(user);
            _context.SaveChanges();

            return ResultViewModel.Success("Usuário atualizado com sucesso.");
        }
    }
}

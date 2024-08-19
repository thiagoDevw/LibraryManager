using Library_Manager.Application.Models;
using Library_Manager.Core.Entities;
using Library_Manager.Infrastructure.Persistence;
using Models.ModelsUsers;

namespace Library_Manager.Application.Services
{
    public interface IUserService
    {
        ResultViewModel<UserDetailsModel> GetUserById(int id); 
        ResultViewModel<User> CreateUser(CreateUserModels model);
        ResultViewModel UpdateUser(int id, UpdateUserModel model);
        ResultViewModel DeleteUser(int id);
    }

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
            throw new NotImplementedException();
        }

        public ResultViewModel<UserDetailsModel> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public ResultViewModel UpdateUser(int id, UpdateUserModel model)
        {
            throw new NotImplementedException();
        }
    }
}

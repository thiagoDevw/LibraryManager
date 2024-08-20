using Library_Manager.Application.Models;
using Library_Manager.Core.Entities;
using Models.ModelsUsers;

namespace Library_Manager.Application.Services
{
    public interface IUserService
    {
        ResultViewModel<UserDetailsModel> GetUserById(int id); 
        ResultViewModel<User> CreateUser(CreateUserModels model);
        ResultViewModel UpdateUser(int id, UpdateUserModel model);
        ResultViewModel DeleteUser(int id);
        bool EmailExists(string email);
    }
}

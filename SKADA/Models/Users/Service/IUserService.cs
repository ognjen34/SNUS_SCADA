using SKADA.Models.Users.Model;

namespace SKADA.Models.Users.Service
{
    public interface IUserService
    {
        Task AddUser(DTOS.CreateUserDTO user);
        Task<User> UpdateUser(int id, User user);
        Task<bool> UserExists(string email);
        Task<User> GetById(int id);
        Task DeleteUser(int id);
        Task<User> GetByEmail(string email);

    }
}

using SKADA.Models.Users.Model;

namespace SKADA.Models.Users.Service
{
    public interface IUserService
    {
        Task AddUser(DTOS.CreateUserDTO user);
        Task<User> UpdateUser(Guid id, User user);
        Task<bool> UserExists(string email);
        Task<User> GetById(Guid id);
        Task DeleteUser(Guid id);
        Task<User> GetByEmail(string email);

    }
}

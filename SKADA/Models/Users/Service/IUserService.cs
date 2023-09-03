using SKADA.Models.DTOS;
using SKADA.Models.Users.Model;

namespace SKADA.Models.Users.Service
{
    public interface IUserService
    {
        Task AddUser(CreateUserDTO user);
        Task<User> UpdateUser(CreateUserDTO user);
        Task<bool> UserExists(string email);
        Task<User> GetById(Guid id);
        Task DeleteUser(Guid id);
        Task<User> GetByEmail(string email);
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> GetClients();
        List<User> GetAllAdmins();

    }
}

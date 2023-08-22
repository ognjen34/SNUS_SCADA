using SKADA.Models.Users.Model;

namespace SKADA.Models.Users.Repository
{
    public interface IUserRepository<T> where T : User
    {
        Task<T> GetByEmail(string email);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T user);
        Task Update(T user);
        Task Delete(T user);
    }
}

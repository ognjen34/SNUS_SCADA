using SKADA.Models.Users.Model;

namespace SKADA.Models.Users.Repository
{
    public interface IUserRepository<T> where T : User
    {
        Task<T> GetByEmail(string email);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetClients();
        Task<T> GetById(Guid id);
        Task Add(T user);
        Task Update(T user);
        Task Delete(T user);
        Task<IEnumerable<T>> GetUsersByAnalogDataId(Guid analogDataId);
        Task<IEnumerable<T>> GetUsersByDigitalDataId(Guid digitalDataId);

    }
}

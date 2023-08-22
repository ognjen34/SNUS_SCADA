using Microsoft.EntityFrameworkCore;
using SKADA.Models.Users.Model;

namespace SKADA.Models.Users.Repository
{
    public class UserRepository<T> : IUserRepository<T> where T : User
    {
        private readonly DbSet<T> _users;
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
            _users = context.Set<T>();
        }


        public async Task<T> GetById(int id)
        {
            return await _users.FirstOrDefaultAsync(user => user.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _users.ToListAsync();
        }

        public async Task Add(T user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T user)
        {
            _users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T user)
        {
            _users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByEmail(string email)
        {
            return await _users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}

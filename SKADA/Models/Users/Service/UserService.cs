using SKADA.Models.DTOS;
using SKADA.Models.Users.Model;
using SKADA.Models.Users.Repository;

namespace SKADA.Models.Users.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<User> _userRepository;

        public UserService(IUserRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUser(CreateUserDTO userDTO)
        {
            User user = new User();
            user.Name = userDTO.Name;
            user.Surname = userDTO.Surname;
            user.Email = userDTO.Email;
            user.Password = userDTO.Password;
            user.Role = UserType.CLIENT;

            await _userRepository.Add(user);
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user != null)
            {
                await _userRepository.Delete(user);
               
            }
            else
            {
                Console.WriteLine("No such user");
            }
        }

        public async Task<User> GetByEmail(string email)
        {

            User user = await _userRepository.GetByEmail(email);

            if (user == null)
            {
                Console.WriteLine("No such user");
            }
            

            return user;
        }

        public async Task<User> GetById(Guid id)
        {
            User user = await _userRepository.GetById(id);

            if (user == null)
            {
                Console.WriteLine("No such user");
            }
            

            return user;
        }

        public async Task<User> UpdateUser(Guid id, User user)
        {
           
            var existingUser = await _userRepository.GetById(id);
            if (existingUser == null)
            {
                
                return null;
            }
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;
            existingUser.Surname = user.Surname;
            existingUser.Name = user.Name;
            await _userRepository.Update(existingUser);
            return existingUser;
        }

        public async Task<bool> UserExists(string email)
        {

            User user = await _userRepository.GetByEmail(email);

            if (user == null)
            {

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

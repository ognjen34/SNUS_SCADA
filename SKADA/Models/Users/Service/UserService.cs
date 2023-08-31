using SKADA.Models.DTOS;
using SKADA.Models.Inputs.Model;
using SKADA.Models.Inputs.Repository;
using SKADA.Models.Users.Model;
using SKADA.Models.Users.Repository;

namespace SKADA.Models.Users.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IDigitalInputRepository _ditalInputRepository;

        public UserService(IUserRepository<User> userRepository, IAnalogInputRepository analogInputRepository, IDigitalInputRepository digitalInputRepository)
        {
            _userRepository = userRepository;
            _analogInputRepository = analogInputRepository;
            _ditalInputRepository = digitalInputRepository;
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

        public  List<User> GetAllAdmins()
        {
            return _userRepository.GetAll().Result.Where(user => user.Role == UserType.ADMIN).ToList();
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

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
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

        public async Task<IEnumerable<User>> GetClients()
        {
            return await _userRepository.GetClients();
        }

        public async Task<User> UpdateUser(CreateUserDTO user)
        {
            List<AnalogInput> newAnalogInputs = new List<AnalogInput>();
            List<DigitalInput> newDigitalInputs = new List<DigitalInput>();

            var existingUser = await _userRepository.GetByEmail(user.Email);
            if (existingUser == null)
            {
                
                return null;
            }
            existingUser.Email = user.Email;
            existingUser.Surname = user.Surname;
            existingUser.Name = user.Name;
            foreach(String analogId in user.AnalogInputsIds)
            {
                AnalogInput analogInput = await _analogInputRepository.Get(new Guid(analogId));
                newAnalogInputs.Add(analogInput);
                

                
            }
            foreach (String digitalId in user.DigitalInputsIds)
            {

                DigitalInput digitalInput = await _ditalInputRepository.Get(new Guid(digitalId));
                newDigitalInputs.Add(digitalInput);

            }
            existingUser.analogInputs = newAnalogInputs;
            existingUser.digitalInputs = newDigitalInputs;
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

using Microsoft.AspNetCore.SignalR;
using SKADA.Models.Users.Model;
using SKADA.Models.Users.Repository;
using System.Security.Claims;

namespace SKADA.Models.Alarms.Hubs
{
    public class AlarmSocket : Hub<IAlarmClient>
    {
        private readonly IUserRepository<User> _userRepository;
        public AlarmSocket(IUserRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var userEmail = Context.User.FindFirst(ClaimTypes.Name)?.Value;
            Console.WriteLine("CONNECTION ESTABLISHED: USER WITH EMAIL:", userEmail);

            if (!string.IsNullOrEmpty(userEmail))
            {
                User user = _userRepository.GetByEmail(userEmail).Result;
                Console.WriteLine(user.Id.ToString());
                await Groups.AddToGroupAsync(Context.ConnectionId, user.Id.ToString());
            }

            await base.OnConnectedAsync();
        }
    }
}

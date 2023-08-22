using Microsoft.AspNetCore.Mvc;
using SKADA.Models.DTOS;
using SKADA.Models.Users.Model;
using SKADA.Models.Users.Service;

namespace SKADA.Models.Users.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            User user = await _userService.GetByEmail(model.Username);
            if (user == null)
            {
                return NotFound("User not found");
            }
            else
            {
                if(user.Password != model.Password)
                {
                    return BadRequest("Credetials are not good");
                }
                
            }
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> AddUser([FromBody] CreateUserDTO user)
        {
            

            if (!_userService.UserExists(user.Email).Result)
            {
                await _userService.AddUser(user);
               

                return Ok();
            }
            else
            {
                return Conflict("USER WITH THIS EMAIL ALREADY EXISTS");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            var existingUser = await _userService.UpdateUser(id, user);
            if (existingUser == null)
            {
                

                return NotFound();
            }

            return Ok();
        }

        [HttpPost("email")]
        public async Task<User> GetByEmail([FromBody] UserEmailDTO userEmailDTO)
        {


            User user = await _userService.GetByEmail(userEmailDTO.email);

            if (user != null)
            {
                Console.WriteLine("User found");
            }
            else
            {
                Console.WriteLine("User found");
            }

            return user;
        }

    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SKADA.Models.DTOS;
using SKADA.Models.Users.Model;
using SKADA.Models.Users.Service;
using System.Security.Claims;

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

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            User user = await _userService.GetByEmail(model.Username);
            
            if(user == null)
            {
                return BadRequest("User with this email does not exist");

            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

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

        [HttpPut("")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var existingUser = await _userService.UpdateUser(user.Id, user);
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

        [HttpGet("authorized")]
        [Authorize(Policy = "ClientOnly")]

        public async Task<IActionResult> GetAuthorizedDataAsync()
        {

            var userEmail = User.FindFirstValue(ClaimTypes.Name);

            User user = await _userService.GetByEmail(userEmail);

            if (user == null)
            {
                return Forbid();
            }

            return Ok(user);
        }

        [HttpGet("users")]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userService.GetAll();
        }
        [HttpGet("clients")]
        public async Task<IEnumerable<User>> GetClients()
        {
            return await _userService.GetClients();
        }
    }
}

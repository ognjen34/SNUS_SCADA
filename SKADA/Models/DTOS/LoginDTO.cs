using System.ComponentModel.DataAnnotations;

namespace SKADA.Models.DTOS
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}

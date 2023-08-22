using System.ComponentModel.DataAnnotations;

namespace SKADA.Models.DTOS
{
    public class UserEmailDTO
    {
        [Required]
        public string email { get; set; }
    }
}

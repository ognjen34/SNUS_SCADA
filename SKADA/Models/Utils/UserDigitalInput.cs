using SKADA.Models.Inputs.Model;
using SKADA.Models.Users.Model;

namespace SKADA.Models.Utils
{
    public class UserDigitalInput
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int DigitalInputId { get; set; }
        public DigitalInput DigitalInput { get; set; }
    }
}

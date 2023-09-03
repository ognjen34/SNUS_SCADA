using SKADA.Models.Inputs.Model;
using SKADA.Models.Users.Model;

namespace SKADA.Models.Utils
{
    public class UserAnalogInput
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int AnalogInputId { get; set; }
        public AnalogInput AnalogInput { get; set; }
    }
}

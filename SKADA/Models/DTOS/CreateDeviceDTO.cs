using SKADA.Models.Devices.Model;
using static SKADA.Models.Devices.Model.Device;

namespace SKADA.Models.DTOS
{
    public class CreateDeviceDTO
    {
        public String IOAdress { get; set; }
        public DeviceType deviceType { get; set; }
        public DeviceConfig deviceConfig { get; set; }
    }
}

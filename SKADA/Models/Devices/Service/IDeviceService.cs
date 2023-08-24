using SKADA.Models.Devices.Model;

namespace SKADA.Models.Devices.Service
{
    public interface IDeviceService
    {
        Task Simulation();
        Task<List<String>> GetAllDeviceAddresses();
        Task Add(Device device);
    }
}

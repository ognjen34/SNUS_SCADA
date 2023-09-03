using SKADA.Models.Devices.Model;

namespace SKADA.Models.Devices.Service
{
    public interface IDeviceService
    {
        Task Simulation();
        Task<List<String>> GetAllDeviceAddresses();
        Task<IEnumerable<Device>> GetDevices();
        Task Add(Device device);
        Task Delete(string id);
    }
}

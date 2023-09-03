using SKADA.Models.Devices.Model;

namespace SKADA.Models.Devices.Repository
{
    public interface IDeviceRepository
    {
        Task<Device> GetById(Guid id);
        Task<IEnumerable<Device>> GetAll();
        Task<Device> GetByIOAddress(String address);
        Task Add(Device device);
        Task Update(Device device);
        Task Delete(Device device);
    }
}

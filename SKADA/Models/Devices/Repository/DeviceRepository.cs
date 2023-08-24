using SKADA.Models.Devices.Model;

namespace SKADA.Models.Devices.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        protected AppDbContext _context;

        public DeviceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Device device)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            try
            {
                _context.Device.Add(device);
                _context.SaveChanges();
                await Task.Delay(1);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
        }

        public async Task Delete(Device device)
        {
            Device device1;
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                device1 = _context.Device.FirstOrDefault(e => e.Id == device.Id);
                if (device1 != null)
                {
                    _context.Device.Remove(device);
                    _context.SaveChanges();
                    await Task.Delay(1);
                }
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
        }

        public async Task<IEnumerable<Device>> GetAll()
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            IEnumerable<Device> devices;
            try
            {
                devices = _context.Device.ToList();
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return devices;
        }

        public async Task<Device> GetById(Guid id)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            Device device;
            try
            {
                device = _context.Device.FirstOrDefault(e => e.Id == id);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return device;
        }

        public async Task<Device> GetByIOAddress(string address)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            Device device;
            try
            {
                device = _context.Device.FirstOrDefault(e => e.IOAdress == address);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return device;
        }

        public async Task Update(Device device)
        {
            Device oldDevice;
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                oldDevice = _context.Device.FirstOrDefault(e => e.Id == device.Id);
                if (oldDevice != null)
                {
                    _context.Entry(oldDevice).CurrentValues.SetValues(device);
                    _context.SaveChanges();
                    await Task.Delay(1);
                }
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
        }
    }
}

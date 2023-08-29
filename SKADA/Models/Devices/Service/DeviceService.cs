using SKADA.Models.Devices.Model;
using SKADA.Models.Devices.Repository;
using SKADA.Models.Inputs.Model;
using SKADA.Models.Inputs.Repository;

namespace SKADA.Models.Devices.Service
{

    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IDigitalInputRepository _digitalInputRepository;



        public DeviceService(IDeviceRepository deviceRepository, IAnalogInputRepository analogInputRepository, IDigitalInputRepository digitalInputRepository)
        {
            _deviceRepository = deviceRepository;
            _analogInputRepository = analogInputRepository;
            _digitalInputRepository = digitalInputRepository;

        }

        public async Task Add(Device device)
        {
            device.Value = 1;
            await _deviceRepository.Add(device);
            StartDeviceSimulation(device);
        }

        public async Task<List<string>> GetAllDeviceAddresses()
        {
            return (await _deviceRepository.GetAll()).Select(device => device.IOAdress).ToList();
        }
        public async Task StartDeviceSimulation(Device device)
        {
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;
                Random r = new Random();

                while (true)
                {


                    List<DigitalInput> digitalInputTry = _digitalInputRepository.GetByIOAddress(device.IOAdress).Result.ToList();
                    if (digitalInputTry.Count != 0)
                    {
                        if (device.deviceType == Device.DeviceType.SIMULATION)
                        {
                            switch (device.deviceConfig.SimulationType)
                            {
                                case SimulationType.SIN:
                                    device.Value = Sine() > 0 ? 1 : 0;
                                    break;
                                case SimulationType.COS:
                                    device.Value = Cosine() > 0 ? 1 : 0;
                                    break;
                                case SimulationType.RAMP:
                                    device.Value = Ramp() > 50 ? 1 : 0;
                                    break;
                            }
                        }
                        if (device.deviceType == Device.DeviceType.RTU)
                        {
                            if (r.NextDouble() > 0.5)
                                device.Value = 1;
                            else
                                device.Value = 0;
                        }
                    }
                    List<AnalogInput> analogInputTry = _analogInputRepository.GetByIOAddress(device.IOAdress).Result.ToList();
                    if (analogInputTry.Count != 0)
                    {
                        if (device.deviceType == Device.DeviceType.SIMULATION)
                        {
                            switch (device.deviceConfig.SimulationType)
                            {
                                case SimulationType.SIN:
                                    device.Value = Sine();
                                    break;
                                case SimulationType.COS:
                                    device.Value = Cosine();
                                    break;
                                case SimulationType.RAMP:
                                    device.Value = Ramp();
                                    break;
                            }
                        }
                        if (device.deviceType == Device.DeviceType.RTU)
                        {
                            double newValue = device.Value * (r.NextDouble() * 0.4 + 0.8);
                            if (Math.Abs(device.Value - newValue) < 0.01)
                                device.Value = (device.deviceConfig.LowLimit + device.deviceConfig.HighLimit) * 0.5;
                            else
                            {
                                device.Value = newValue;
                            }
                        }
                    }
                    Console.WriteLine(device.IOAdress + "  " + device.Value);

                    Thread.Sleep(Globals.Globals.DeviceRefresh);

                }
            }).Start();

        }
        

        public async Task Simulation()
        {
            List<Device> devices = (await _deviceRepository.GetAll()).ToList();
            foreach (Device device in devices)
            {
                StartDeviceSimulation(device);
                
            }
        }

        private static double Sine()
        {
            return 100 * Math.Sin((double)DateTime.Now.Second / 60 * Math.PI);
        }

        private static double Cosine()
        {
            return 100 * Math.Cos((double)DateTime.Now.Second / 60 * Math.PI);
        }

        private static double Ramp()
        {
            return 100 * DateTime.Now.Second / 60;
        }

        public Task<IEnumerable<Device>> GetDevices()
        {
            return _deviceRepository.GetAll();
        }

        public async Task Delete(string id)
        {
            Guid  guid = Guid.Parse(id);
            Device device = await _deviceRepository.GetById(guid);
            _deviceRepository.Delete(device);
        }
    }
}

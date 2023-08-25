using SKADA.Models.Devices.Model;
using SKADA.Models.Devices.Repository;
using SKADA.Models.Inputs.Model;
using SKADA.Models.Inputs.Repository;

namespace SKADA.Models.Inputs.Service
{
    public class DigitalInputService : IDigitalInputService
    {

        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IDigitalInputRepository _digitalInputRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDigitalReadInstanceRepository _digitalReadInstanceRepository;


        public DigitalInputService(IAnalogInputRepository analogInputRepository, IDigitalInputRepository digitalInputRepository,
            IDeviceRepository deviceRepository,IDigitalReadInstanceRepository digitalReadInstanceRepository)
        {
            _analogInputRepository = analogInputRepository;
            _digitalInputRepository = digitalInputRepository;
            _deviceRepository = deviceRepository;
            _digitalReadInstanceRepository =  digitalReadInstanceRepository;


        }

        public async Task Create(DigitalInput input)
        {
            await _digitalInputRepository.Create(input);
        }

        public async Task startDigitalDataReading()
        {
            foreach(DigitalInput di in _digitalInputRepository.GetAll().Result.ToList())
            {
                if (di.Scan)
                {
                    readSingleDigitalData(di.Id);
                }
            }
        }
        public Task<IEnumerable<DigitalInput>> GetAll()
        {
            return _digitalInputRepository.GetAll();
        }

        private async Task readSingleDigitalData(Guid tagId)
        {
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    DigitalInput digitalInput = await _digitalInputRepository.Get(tagId);
                    if (digitalInput == null)
                        break;

                    if (digitalInput.Scan)
                    {

                        Device device = _deviceRepository.GetByIOAddress(digitalInput.IOAddress).Result;
                        DigitalReadInstance ioDigitalData = new DigitalReadInstance
                        (
                            new Guid(),
                            device.IOAdress,
                            device.Value,
                            DateTime.Now,
                            digitalInput.Id
                        );
                        await _digitalReadInstanceRepository.Create(ioDigitalData);
                        Console.WriteLine(("READING DIGITAL:" + device.IOAdress + "  " + ioDigitalData.Value));

                    }
                    else
                    {
                        break;
                    }
                    Thread.Sleep(digitalInput.ScanTime * 1000);

                }
            }).Start();
        }
    }
}

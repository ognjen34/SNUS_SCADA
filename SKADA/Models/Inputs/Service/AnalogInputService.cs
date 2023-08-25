using SKADA.Models.Devices.Model;
using SKADA.Models.Devices.Repository;
using SKADA.Models.Inputs.Model;
using SKADA.Models.Inputs.Repository;

namespace SKADA.Models.Inputs.Service
{
    public class AnalogInputService : IAnalogInputService
    {

        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IAnalogReadInstanceRepository _analogReadInstanceRepository;
        public AnalogInputService(IAnalogInputRepository analogInputRepository, IDigitalInputRepository digitalInputRepository,IDeviceRepository deviceRepository, IAnalogReadInstanceRepository analogReadInstanceRepository)
        {
            _analogInputRepository = analogInputRepository;
            _deviceRepository = deviceRepository;
            _analogReadInstanceRepository = analogReadInstanceRepository;
            
        }
        public async Task Create(AnalogInput input)
        {
            await _analogInputRepository.Create(input);
            
        }

        public async Task Delete(Guid id)
        {
            await _analogInputRepository?.Delete(id);
        }

        public Task<AnalogInput> Get(Guid id)
        {
            return _analogInputRepository.Get(id);
        }

        public Task<IEnumerable<AnalogInput>> GetAll()
        {
            return _analogInputRepository.GetAll();
        }

        public async Task Update(AnalogInput input)
        {
            await _analogInputRepository.Update(input);
        }
        public async Task startDigitalDataReading()
        {
            foreach (AnalogInput di in _analogInputRepository.GetAll().Result.ToList())
            {
                if (di.Scan)
                {
                    readSingleAnalogData(di.Id);
                }
            }
        }

        private async Task readSingleAnalogData(Guid tagId)
        {
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    AnalogInput analogInput = await _analogInputRepository.Get(tagId);
                    if (analogInput == null)
                        break;

                    if (analogInput.Scan)
                    {

                        Device device = _deviceRepository.GetByIOAddress(analogInput.IOAddress).Result;
                        AnalogReadInstance ioAnalogData = new AnalogReadInstance
                        (
                            new Guid(),
                            device.IOAdress,
                            device.Value,
                            DateTime.Now,
                            analogInput.Id
                        );
                        await _analogReadInstanceRepository.Create(ioAnalogData);

                    }
                    else
                    {
                        break;
                    }
                    Thread.Sleep(analogInput.ScanTime * 1000);

                }
            }).Start();
        }
    }
}

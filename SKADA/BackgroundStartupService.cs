using SKADA.Models.Devices.Service;
using SKADA.Models.Inputs.Service;

namespace SKADA
{
    public class BackgroundStartupService : BackgroundService
    {
        private readonly IAnalogInputService _analogInputService;
        private readonly IDigitalInputService _digitalInputService;
        private readonly IDeviceService _deviceService;
        public BackgroundStartupService(IAnalogInputService analogInputService, IDigitalInputService digitalInputService, IDeviceService deviceService)
        {
            _analogInputService = analogInputService;
            _digitalInputService = digitalInputService;
            _deviceService = deviceService;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _deviceService.Simulation();
                _digitalInputService.startDigitalDataReading();
                _analogInputService.startAnalogDataReading();

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}

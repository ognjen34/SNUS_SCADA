using SKADA.Models.Devices.Model;
using SKADA.Models.Devices.Service;

    namespace SKADA.Models.Utils
    {
        public class ShutdownService : IHostedService, IDisposable
        {
            private readonly IDeviceService _deviceService;

            public ShutdownService(IDeviceService deviceService)
            {
                _deviceService = deviceService;
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                Console.WriteLine("SERVICE STARTED");
                return Task.CompletedTask;
            }

            public async Task StopAsync(CancellationToken cancellationToken)
            {
                Console.WriteLine("FKASOFPKASOKSAP");
                var devices = _deviceService.GetDevices();

                var deviceXml = XMLSerializer.SerializeToXml<Device>(devices.Result);

                await File.WriteAllTextAsync("SCADA_config.xml", deviceXml);
            }

            public void Dispose()
            {
                // Dispose resources here if needed.
            }
        }
    }

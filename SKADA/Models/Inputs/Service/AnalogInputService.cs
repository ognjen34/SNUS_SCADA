using Microsoft.AspNetCore.SignalR;
using SKADA.Models.Alarms.Hubs;
using SKADA.Models.Alarms.Model;
using SKADA.Models.Alarms.Repository;
using SKADA.Models.Devices.Model;
using SKADA.Models.Devices.Repository;
using SKADA.Models.Inputs.Hubs;
using SKADA.Models.Inputs.Model;
using SKADA.Models.Inputs.Repository;
using SKADA.Models.Users.Model;
using SKADA.Models.Users.Repository;
using static SKADA.Models.Alarms.Model.Alarm;

namespace SKADA.Models.Inputs.Service
{
    public class AnalogInputService : IAnalogInputService
    {

        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IAnalogReadInstanceRepository _analogReadInstanceRepository;
        private readonly IAlarmInstanceRepository _alarmInstanceRepository;
        private readonly IHubContext<TagSocket, ITagClient> _tagSocket;
        private readonly IUserRepository<User> _userRepository;
        private readonly IHubContext<AlarmSocket, IAlarmClient> _alarmSocket;

        public AnalogInputService(IHubContext<AlarmSocket, IAlarmClient> alarmSocket, IUserRepository<User> userRepository,IHubContext<TagSocket, ITagClient> tagSocket,IAnalogInputRepository analogInputRepository, IDigitalInputRepository digitalInputRepository,IDeviceRepository deviceRepository, IAnalogReadInstanceRepository analogReadInstanceRepository,IAlarmInstanceRepository alarmInstanceRepository)
        {
            _alarmSocket =  alarmSocket;
            _userRepository= userRepository;    
            _tagSocket = tagSocket;
            _alarmInstanceRepository = alarmInstanceRepository;
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
        public async Task startAnalogDataReading()
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
                    Console.WriteLine(("READING ANALOG:" + device.IOAdress + "  " + ioAnalogData.Value));
                    await _analogReadInstanceRepository.Create(ioAnalogData);

                    foreach (Alarm analogInputAlarm in analogInput.Alarms)
                    {
                        if ((analogInputAlarm.Type == AlarmType.HIGH &&
                             analogInputAlarm.CriticalValue < device.Value) ||
                            (analogInputAlarm.Type == AlarmType.LOW && analogInputAlarm.CriticalValue > device.Value))
                        {
                                var i = 0;
                                while (i != (int)analogInputAlarm.Priority) {
                                    AlarmInstance alarmAlert = new AlarmInstance
                                    {
                                        Id = Guid.NewGuid(),
                                        AlarmId = analogInputAlarm.Id,
                                        Timestamp = DateTime.Now,
                                        Value = device.Value,
                                        AlarmType = analogInputAlarm.Type.ToString(),
                                        Units = analogInputAlarm.Units,
                                        CriticalValue = analogInputAlarm.CriticalValue,
                                        Message = ""
                                    };
                                    alarmAlert.Message = "alarm " + analogInputAlarm.Id + " critical value for tag " + ioAnalogData.TagId + " at " + alarmAlert.Timestamp + "\n";
                                    await _alarmInstanceRepository.Add(alarmAlert);
                                    await Globals.Globals._fileSemaphore.WaitAsync();
                                    ;
                                    try
                                    {
                                        using (StreamWriter outputfile = new StreamWriter("alarmsLog.txt", true))
                                        {
                                            await outputfile.WriteAsync(alarmAlert.Message);
                                        }
                                    }
                                    finally
                                    {
                                        Globals.Globals._fileSemaphore.Release();
                                    }
                                    Console.WriteLine("ALARMMMMM");
                                    foreach (string userId in _userRepository.GetUsersByAnalogDataId(analogInput.Id).Result.Select(u => u.Id.ToString()).ToList())
                                    {
                                        await _alarmSocket.Clients.Group(userId).ReceiveAlarmData(alarmAlert);

                                    }
                                    i++;
                                }

                        }
                    }
                    foreach (string userId in _userRepository.GetUsersByAnalogDataId(analogInput.Id).Result.Select(u => u.Id.ToString()).ToList())
                    {
                      await _tagSocket.Clients.Group(userId).ReceiveAnalogData(ioAnalogData);

                    }           
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

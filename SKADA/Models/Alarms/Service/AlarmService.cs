using SKADA.Models.Alarms.DTOS;
using SKADA.Models.Alarms.Model;
using SKADA.Models.Alarms.Repository;
using SKADA.Models.Inputs.Model;
using SKADA.Models.Inputs.Repository;
using SKADA.Models.Users.Model;
using SKADA.Models.Users.Repository;
using System.Collections.Generic;

namespace SKADA.Models.Alarms.Service
{
    public class AlarmService : IAlarmService
    {
        private readonly IAlarmRepository _alarmRepository;
        private readonly IAnalogInputRepository _analogInputRepository;
        private readonly IUserRepository<User> _userRepository;
        private readonly IAlarmInstanceRepository _alarmInstanceRepository;

        public AlarmService(IAlarmRepository alarmRepository, IAnalogInputRepository analogInputRepository, IUserRepository<User> userRepository, IAlarmInstanceRepository alarmInstanceRepository)
        {
            _alarmRepository = alarmRepository;
            _analogInputRepository = analogInputRepository;
            _userRepository = userRepository;
            _alarmInstanceRepository = alarmInstanceRepository;
        }


        public async Task makeAlarm(Guid userId, AlarmDTO alarmDto)
        {
            AnalogInput analogInput = await _analogInputRepository.Get(alarmDto.TagId);
            Alarm alarm = new Alarm
            {
                Id = new Guid(),
                CriticalValue = alarmDto.CriticalValue,
                Type = alarmDto.Type,
                Units = analogInput.Units,
                Priority = alarmDto.Priority,
            };

            await _alarmRepository.Add(alarm);
            analogInput.Alarms.Add(alarm);
            await _analogInputRepository.Update(analogInput);

        }

        public async Task deleteAlarm(Guid userId, Guid alarmId, Guid tagId)
        {
            AnalogInput analogInput = await _analogInputRepository.Get(tagId);
            Alarm alarm = await _alarmRepository.GetById(alarmId);

            analogInput.Alarms = analogInput.Alarms.Where(alarm => alarm.Id != alarmId).ToList();
            await _analogInputRepository.Update(analogInput);
            await _alarmRepository.Delete(alarm);
        }

        public async Task<List<AlarmInstance>> GetAllInDateRange(DateTime start, DateTime end)
        {
            List<AlarmInstance> alarms = await _alarmInstanceRepository.GetAllInDateRange(start, end);
            alarms = alarms.OrderBy(alarm => alarm.Timestamp).ToList();
            return alarms;
        }
    }
}

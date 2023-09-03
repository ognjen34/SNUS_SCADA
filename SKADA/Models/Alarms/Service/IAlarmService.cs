using SKADA.Models.Alarms.DTOS;
using SKADA.Models.Alarms.Model;

namespace SKADA.Models.Alarms.Service
{
    public interface IAlarmService
    {
        Task makeAlarm(Guid userId, AlarmDTO alarmDto);
        Task deleteAlarm(Guid userId, Guid alarmId, Guid tagId);
        Task<List<AlarmInstance>> GetAllInDateRange(DateTime start, DateTime end);

    }
}

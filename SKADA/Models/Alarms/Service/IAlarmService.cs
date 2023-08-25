using SKADA.Models.Alarms.DTOS;

namespace SKADA.Models.Alarms.Service
{
    public interface IAlarmService
    {
        Task makeAlarm(Guid userId, AlarmDTO alarmDto);
        Task deleteAlarm(Guid userId, Guid alarmId, Guid tagId);

    }
}

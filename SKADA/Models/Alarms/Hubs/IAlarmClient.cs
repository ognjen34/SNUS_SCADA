using SKADA.Models.Alarms.Model;

namespace SKADA.Models.Alarms.Hubs
{
    public interface IAlarmClient
    {
        Task ReceiveAlarmData(AlarmInstance data);
    }
}

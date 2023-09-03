using static SKADA.Models.Alarms.Model.Alarm;

namespace SKADA.Models.Alarms.DTOS
{
    public interface AlarmDTO
    {
        public AlarmType Type { get; set; }
        public AlarmPriority Priority { get; set; }
        public double CriticalValue { get; set; }
        public Guid TagId { get; set; }
    }
}

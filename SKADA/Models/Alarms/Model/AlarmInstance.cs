using static SKADA.Models.Alarms.Model.Alarm;

namespace SKADA.Models.Alarms.Model
{
    public class AlarmInstance
    {
        public Guid Id { get; set; }
        public Guid AlarmId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public string AlarmType { get; set; }
        public string Units { get; set; }
        public Double CriticalValue { get; set; }
        public Double Value { get; set; }

        public AlarmInstance()
        {

        }

        public AlarmInstance(Guid id, Guid alarmId, DateTime timestamp, string message, string alarmType, string units, double criticalValue, double value)
        {
            Id = id;
            AlarmId = alarmId;
            Timestamp = timestamp;
            Message = message;
            AlarmType = alarmType;
            Units = units;
            CriticalValue = criticalValue;
            Value = value;
        }
    }
}

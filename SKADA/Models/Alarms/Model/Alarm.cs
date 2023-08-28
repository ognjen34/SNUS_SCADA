namespace SKADA.Models.Alarms.Model
{
    public class Alarm
    {
        public Guid Id { get; set; }
        public AlarmType Type { get; set; }
        public AlarmPriority Priority { get; set; }
        public string Units { get; set; }
        public Double CriticalValue { get; set; }

        public Alarm()
        {

        }

        public Alarm(Guid id, AlarmType type, AlarmPriority priority, string units, double criticalValue)
        {
            Id = id;
            Type = type;
            Priority = priority;
            Units = units;
            CriticalValue = criticalValue;
        }

        public enum AlarmType
        {
            LOW,HIGH
        }

        public enum AlarmPriority
        {
            LOW = 1,
            MEDIUM = 2,
            HIGH = 3
        }
    }
}

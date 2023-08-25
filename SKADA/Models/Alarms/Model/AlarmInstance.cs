namespace SKADA.Models.Alarms.Model
{
    public class AlarmInstance
    {
        public Guid Id { get; set; }
        public Guid AlarmId { get; set; }
        public DateTime Timestamp { get; set; }
        public Double Value { get; set; }

        public AlarmInstance()
        {

        }

        public AlarmInstance(Guid id, Guid alarmId, DateTime timestamp, double value)
        {
            Id = id;
            AlarmId = alarmId;
            Timestamp = timestamp;
            Value = value;
        }
    }
}

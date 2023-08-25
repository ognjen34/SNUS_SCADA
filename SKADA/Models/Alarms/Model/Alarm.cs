namespace SKADA.Models.Alarms.Model
{
    public class Alarm
    {
        public Guid Id { get; set; }

        public Alarm(Guid id)
        {
            Id = id;
        }
        public Alarm() { }
    }
}

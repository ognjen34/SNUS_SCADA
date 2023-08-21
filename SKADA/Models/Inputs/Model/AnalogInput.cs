using SKADA.Models.Alarms.Model;
namespace SKADA.Models.Inputs.Model
{
    public class AnalogInput
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Driver { get; set; }
        public string IOAddress { get; set; }
        public int ScanTime { get; set; }
        public bool Scan { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Units { get; set; }
        public List<Alarm> Alarms { get; set; }
    }
}

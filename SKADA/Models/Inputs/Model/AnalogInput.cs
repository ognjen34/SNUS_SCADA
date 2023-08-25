using SKADA.Models.Alarms.Model;
namespace SKADA.Models.Inputs.Model
{
    public class AnalogInput
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public int ScanTime { get; set; }
        public bool Scan { get; set; }
        public string Units { get; set; }
        public List<Alarm> Alarms { get; set; }

        public AnalogInput(Guid id, string description, string iOAddress, int scanTime, bool scan, string units, List<Alarm> alarms)
        {
            Id = id;
            Description = description;
            IOAddress = iOAddress;
            ScanTime = scanTime;
            Scan = scan;
            Units = units;
            Alarms = alarms;
        }
        public AnalogInput() { }
    }
}

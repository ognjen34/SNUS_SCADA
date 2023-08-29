using SKADA.Models.Alarms.Model;

namespace SKADA.Models.DTOS
{
    public class AnalogInputDTO
    {
        public string Description { get; set; }
        public int ScanTime { get; set; }
        public bool Scan { get; set; }
        public string IOAddress { get; set; }
        public string Unit { get; set; }
        public List<AlarmDTO> Alarms { get; set; } // Use AlarmDTO instead of Alarm

        public AnalogInputDTO()
        {
            Alarms = new List<AlarmDTO>();
        }
    }

    public class AlarmDTO
    {
        public int Type { get; set; } // Use int for AlarmType
        public int Priority { get; set; } // Use int for AlarmPriority
        public string Units { get; set; }
        public Double CriticalValue { get; set; }

        public AlarmDTO()
        {

        }
    }
   
}

namespace SKADA.Models.DTOS
{
    public class UpdateAnalogInputDTO
    {
        public string id { get; set; }
        public string Description { get; set; }
        public int ScanTime { get; set; }
        public bool Scan { get; set; }
        public string IOAddress { get; set; }
        public string Unit { get; set; }
        public List<UpdateAlarmDTO> Alarms { get; set; } 

        public UpdateAnalogInputDTO()
        {
            Alarms = new List<UpdateAlarmDTO>();
        }
    }

    public class UpdateAlarmDTO
    {
        public string id { get; set; }
        public int Type { get; set; } 
        public int Priority { get; set; } 
        public string Units { get; set; }
        public Double CriticalValue { get; set; }

        public UpdateAlarmDTO()
        {

        }
    }
}

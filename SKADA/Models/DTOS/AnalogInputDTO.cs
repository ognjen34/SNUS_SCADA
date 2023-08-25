namespace SKADA.Models.DTOS
{
    public class AnalogInputDTO
    {
        public string Description { get; set; }
        public int ScanTime { get; set; }
        public bool Scan { get; set; }

        public string IOAddress { get; set; }
        public string Unit { get; set; }

        public AnalogInputDTO(string description, int scanTime, bool scanOn, string unit)
        {
            Description = description;
            ScanTime = scanTime;
            Scan = scanOn;
            Unit = unit;
        }

        public AnalogInputDTO()
        {
        }
    }
}

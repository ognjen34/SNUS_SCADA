namespace SKADA.Models.DTOS
{
    public class DigitalInputDTO
    {
        public string Description { get; set; }
        public int ScanTime { get; set; }
        public bool Scan { get; set; }

        public string IOAddress { get; set; }

        public DigitalInputDTO(string description, int scanTime, bool scanOn)
        {
            Description = description;
            ScanTime = scanTime;
            Scan = scanOn;
       
        }

        public DigitalInputDTO()
        {
        }
    }
}

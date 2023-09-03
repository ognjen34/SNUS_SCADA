namespace SKADA.Models.Inputs.Model
{
    public class DigitalInput
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public int ScanTime { get; set; }
        public bool Scan { get; set; }

        public DigitalInput()
        {

        }

        public DigitalInput(Guid id, string description, string iOAddress, int scanTime,bool scan)
        {
            Id = id;
            Description = description;
            IOAddress = iOAddress;
            ScanTime = scanTime;
            Scan = scan;
        }

    }
}

namespace SKADA.Models.Inputs.Model
{
    public class DigitalInput
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Driver { get; set; }
        public string IOAddress { get; set; }
        public int ScanTime { get; set; }
        public bool Scan { get; set; }
    }
}

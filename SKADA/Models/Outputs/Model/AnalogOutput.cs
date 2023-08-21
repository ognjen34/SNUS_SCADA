namespace SKADA.Models.Outputs.Model
{
    public class AnalogOutput
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public double InitialValue { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Units { get; set; }

    }
}

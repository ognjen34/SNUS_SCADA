namespace SKADA.Models.Outputs.Model
{
    public class DigitalOutput
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public double InitialValue { get; set; }
    }
}

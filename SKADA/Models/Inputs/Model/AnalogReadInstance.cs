namespace SKADA.Models.Inputs.Model
{
    public class AnalogReadInstance 
    {
        public Guid Id { get; set; }
        public String IOAddress { get; set; }
        public Double Value { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid TagId { get; set; }
        public AnalogReadInstance()
        {

        }

        public AnalogReadInstance(Guid id, string iOAddress, double value, DateTime timestamp, Guid tagId)
        {
            Id = id;
            IOAddress = iOAddress;
            Value = value;
            Timestamp = timestamp;
            TagId = tagId;
        }

    }
}

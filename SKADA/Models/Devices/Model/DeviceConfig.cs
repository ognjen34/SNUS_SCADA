namespace SKADA.Models.Devices.Model
{
    public class DeviceConfig
    {
        public double LowLimit { get; set; }    
        public double HighLimit { get; set; }
        public SimulationType SimulationType { get; set; }
        
        public DeviceConfig()
        {

        }

        public DeviceConfig(int lowLimit, int highLimit, SimulationType simulationType)
        {
            LowLimit = lowLimit;
            HighLimit = highLimit;
            SimulationType = simulationType;
        }
    }


    public enum SimulationType
    {
        SIN,
        COS,
        RAMP,
        RTU
    }
}

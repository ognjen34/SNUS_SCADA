namespace SKADA.Models.Devices.Model
{
    public class DeviceConfig
    {
        public int LowLimit { get; set; }    
        public int HighLimit { get; set; }
        public SimulationType SimulationType { get; set; }
    }

    public enum SimulationType
    {
        SIN,
        COS,
        RAMP,
        RTU
    }
}

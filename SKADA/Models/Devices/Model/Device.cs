﻿namespace SKADA.Models.Devices.Model
{
    public class Device
    {
        public Guid Id { get; set; }
        public String IOAdress { get; set; }
        public DeviceType deviceType { get; set; }
        public Double Value { get; set; }
        public DeviceConfig deviceConfig { get; set; }

        public Device(string ioadress, double value, DeviceType deviceType)
        {
            Id = Guid.NewGuid();
            IOAdress = ioadress;
            Value = value;
            this.deviceType = deviceType;
        }

        public enum DeviceType
        {
            SIMULATION,
            RTU
        }
        public Device()
        {
            
        }
    }

}

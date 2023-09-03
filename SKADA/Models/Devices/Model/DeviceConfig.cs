using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SKADA.Models.Devices.Model
{
    public class DeviceConfig:IXmlSerializable
    {
        [XmlElement]
        public double LowLimit { get; set; }
        [XmlElement]
        public double HighLimit { get; set; }
        [XmlElement]
        public SimulationType SimulationType { get; set; }
        
        public DeviceConfig()
        {

        }
        public void WriteXml(XmlWriter writer)
        {
            if (SimulationType == SimulationType.RTU)
            {
                writer.WriteElementString("LowLimit", LowLimit.ToString());
                writer.WriteElementString("HighLimit", HighLimit.ToString());
            }

            writer.WriteElementString("SimulationType", SimulationType.ToString());
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement("DeviceConfig");
            if (reader.IsStartElement("LowLimit"))
            {
                LowLimit = double.Parse(reader.ReadElementString("LowLimit"));
            }
            else
            {
                LowLimit = -1;
            }

            Console.WriteLine("2");
            if (reader.IsStartElement("HighLimit"))
            {
                HighLimit = double.Parse(reader.ReadElementString("HighLimit"));
            }
            else
            {
                HighLimit = -1;
            }

            if (reader.IsStartElement("SimulationType"))
            {
                SimulationType = (SimulationType)Enum.Parse(typeof(SimulationType), reader.ReadElementString("SimulationType"));
            }
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        XmlSchema? IXmlSerializable.GetSchema()
        {
            throw new NotImplementedException();
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

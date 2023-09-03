using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SKADA.Models.Devices.Model
{
    public class Device : IXmlSerializable
    {
        [XmlElement]
        public Guid Id { get; set; }
        [XmlElement]
        public String IOAdress { get; set; }
        [XmlElement]
        public DeviceType deviceType { get; set; }
        [XmlIgnore]
        public Double Value { get; set; }
        [XmlElement]
        public DeviceConfig deviceConfig { get; set; }

        public Device(string ioadress, double value, DeviceType deviceType, DeviceConfig deviceConfig)
        {
            Id = Guid.NewGuid();
            IOAdress = ioadress;
            Value = value;
            this.deviceType = deviceType;
            this.deviceConfig = deviceConfig;
        }

        public enum DeviceType
        {
            SIMULATION,
            RTU
        }

        public Device()
        {

        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("Id", Id.ToString());
            writer.WriteElementString("IOAdress", IOAdress);
            writer.WriteElementString("DeviceType", deviceType.ToString());

            if (deviceConfig != null)
            {
                writer.WriteStartElement("DeviceConfig");
                deviceConfig.WriteXml(writer);
                writer.WriteEndElement();
            }
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement("Device");

            Id = Guid.Parse(reader.ReadElementString("Id"));
            IOAdress = reader.ReadElementString("IOAdress");
            deviceType = (DeviceType)Enum.Parse(typeof(DeviceType), reader.ReadElementString("DeviceType"));
            Value = 0;

            if (reader.IsStartElement("DeviceConfig"))
            {
                deviceConfig = new DeviceConfig();
                deviceConfig.ReadXml(reader);
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
    }
}

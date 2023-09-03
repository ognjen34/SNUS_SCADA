using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SKADA.Models.Utils
{
    public class XMLSerializer
    {
        public static string SerializeToXml<T>(IEnumerable<T> items)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter())
            {
                var settings = new XmlWriterSettings
                {
                    Indent = true,            // Add indentation
                    NewLineChars = "\r\n",    // Use Windows-style new lines
                    NewLineHandling = NewLineHandling.Replace
                };

                using (var xmlWriter = XmlWriter.Create(writer, settings))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement(typeof(T).Name + "s");

                    foreach (var item in items)
                    {
                        serializer.Serialize(xmlWriter, item);
                    }

                    xmlWriter.WriteEndElement(); // Close the root element
                    xmlWriter.WriteEndDocument();
                }

                return writer.ToString();
            }
        }

        public static List<T> DeserializeFromXml<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xml))
            {
                var xmlDoc = XDocument.Load(reader);
                var elements = xmlDoc.Root.Elements("Device").ToList();

                try
                {
                    var result = new List<T>();
                    foreach (var element in elements)
                    {
                        using (var elementReader = element.CreateReader())
                        {
                            var item = (T)serializer.Deserialize(elementReader);
                            result.Add(item);
                        }
                    }
                    return result;
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine("Deserialization Error: " + ex.Message);
                    return null;
                }
            }
        }









    }


}
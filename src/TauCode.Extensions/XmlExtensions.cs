using System.IO;
using System.Text;
using System.Xml;

namespace TauCode.Extensions
{
    public static class XmlExtensions
    {
        public static string ToXmlString(this XmlDocument document)
        {
            using (MemoryStream stream = new MemoryStream())
            using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8))
            using (StreamReader reader = new StreamReader(stream))
            {
                writer.Formatting = Formatting.Indented;
                document.WriteContentTo(writer);

                writer.Flush();
                stream.Flush();

                stream.Position = 0;
                var xmlString = reader.ReadToEnd();

                return xmlString;
            }
        }
    }
}

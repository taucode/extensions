using System.IO;
using System.Text;
using System.Xml;

namespace TauCode.Extensions
{
// todo move to TauCode.Xml package
    public static class XmlExtensions
    {
        public static string ToXmlString(this XmlDocument document)
        {
            using var stream = new MemoryStream();
            using var writer = new XmlTextWriter(stream, Encoding.UTF8);
            using var reader = new StreamReader(stream);

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

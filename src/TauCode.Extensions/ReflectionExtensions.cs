using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace TauCode.Extensions
{
    public static class ReflectionExtensions
    {
        public static object GetFieldValue(this object instance, string fieldName)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentException("Invalid field name.", nameof(fieldName));
            }

            var bindFlags =
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static;

            var field = instance.GetType().GetField(fieldName, bindFlags);

            if (field == null)
            {
                throw new InvalidOperationException($"Field not found: '{fieldName}'.");
            }

            return field.GetValue(instance);
        }

        public static void SetFieldValue(this object instance, string fieldName, object newValue)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentException("Invalid field name.", nameof(fieldName));
            }

            var bindFlags =
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static;

            var field = instance.GetType().GetField(fieldName, bindFlags);

            if (field == null)
            {
                throw new InvalidOperationException($"Field not found: '{fieldName}'.");
            }

            field.SetValue(instance, newValue);
        }

        public static string FindFullResourceName(this Assembly assembly, string localResourceName)
        {
            var matches = assembly
                .GetManifestResourceNames()
                .Where(x => x.EndsWith(localResourceName, StringComparison.InvariantCulture))
                .ToList();

            if (matches.Count == 0)
            {
                throw new FileNotFoundException($"Resource not found: '{localResourceName}'.");
            }

            if (matches.Count > 1)
            {
                throw new InvalidOperationException($"More than one resource found: '{localResourceName}'.");
            }

            return matches.Single();
        }

        public static byte[] GetResourceBytes(
            this Assembly assembly,
            string resourceName,
            bool findFullName = false)
        {
            if (findFullName)
            {
                resourceName = assembly.FindFullResourceName(resourceName);
            }

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException("Embedded resource not found.");
                }

                var content = new byte[stream.Length];
                stream.Read(content, 0, content.Length);

                return content;
            }
        }

        public static string GetResourceText(
            this Assembly assembly,
            string resourceName,
            bool findFullName = false)
        {
            var bytes = assembly.GetResourceBytes(resourceName, findFullName);

            using (var stream = new MemoryStream(bytes))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var text = reader.ReadToEnd();
                return text;
            }
        }

        public static string[] GetResourceLines(
            this Assembly assembly,
            string resourceName,
            bool findFullName = false)
        {
            if (findFullName)
            {
                resourceName = assembly.FindFullResourceName(resourceName);
            }

            var txt = assembly.GetResourceText(resourceName);
            var lines = txt.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            return lines;
        }

        public static XmlDocument GetResourceXml(
            this Assembly assembly,
            string resourceName,
            bool findFullName = false)
        {
            if (findFullName)
            {
                resourceName = assembly.FindFullResourceName(resourceName);
            }

            var doc = new XmlDocument();
            var xml = GetResourceText(assembly, resourceName);
            doc.LoadXml(xml);
            return doc;
        }

        public static string SaveResourceToTempFile(
            this Assembly assembly,
            string resourceName,
            bool findFullName = false)
        {
            var tempFilePath = FileTools.CreateTempFilePath();
            var content = assembly.GetResourceBytes(resourceName, findFullName);
            File.WriteAllBytes(tempFilePath, content);
            return tempFilePath;
        }
    }
}

using System;
using System.IO;

namespace TauCode.Extensions
{
    public static class FileTools
    {
// todo: rename to Purge
// todo: move to TauCode.Utility
        public static void ClearDirectory(this DirectoryInfo directory)
        {
            if (directory == null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        public static string CreateTempFilePath(string namePrefix = null, string extension = null)
        {
            namePrefix ??= "ztemp-";
            extension ??= ".dat";

            if (!extension.StartsWith("."))
            {
                extension = $".{extension}";
            }

            var name = $"{namePrefix}{Guid.NewGuid()}{extension}";
            var dir = Path.GetTempPath();

            var path = Path.Combine(dir, name);
            return path;
        }
    }
}

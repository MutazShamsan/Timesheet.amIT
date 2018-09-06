using System.IO;
using System.Linq;
using System.Reflection;

namespace Timesheet.Common
{
    public static class ResourceManagement
    {
        public static Stream GetResourceFileStream(string resourceFileName)
        {
            Stream result = null;
            var assembly = Assembly.GetEntryAssembly();

            if (assembly.GetManifestResourceNames().Any(st => st == resourceFileName))
                result = assembly.GetManifestResourceStream(resourceFileName);

            return result;
        }

        public static string GetCurrentExecution() => Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
    }
}

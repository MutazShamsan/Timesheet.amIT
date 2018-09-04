using System.IO;
using System.Linq;
using System.Reflection;

namespace Timesheet.BusinessLogic
{
    public static class ResourceManagement
    {
        public static Stream GetResourceFileStream(string resourceFileName)
        {
            Stream result = null;
            var assembly = Assembly.GetExecutingAssembly();

            if (assembly.GetManifestResourceNames().Any(st => st == resourceFileName))
                result = assembly.GetManifestResourceStream(resourceFileName);

            return result;
        }
    }
}

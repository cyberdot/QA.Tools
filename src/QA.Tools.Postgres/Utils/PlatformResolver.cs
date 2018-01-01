using System.Runtime.InteropServices;

namespace QA.Tools.Postgres.Utils
{
    public static class PlatformResolver
    {
        public static OSPlatform Resolve()
        {
            var plat = OSPlatform.Create("Unknown");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return OSPlatform.Linux;
            if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return OSPlatform.OSX;
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return OSPlatform.Windows;

            return plat;
        }
    }
}
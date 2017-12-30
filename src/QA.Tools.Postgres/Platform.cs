using System.Runtime.InteropServices;

namespace QA.Tools.Postgres
{
    public static class Platform
    {
        public static OSPlatform? Detect()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OSPlatform.Linux;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OSPlatform.OSX;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OSPlatform.Windows;
            }

            return null;
        }

        public static Architecture Arch()
        {
            return RuntimeInformation.OSArchitecture;
        }
    }
}

using System.Runtime.InteropServices;
using QA.Tools.Postgres.Utils;

namespace QA.Tools.Postgres
{
    public class Distribution
    {
        public Distribution(Version version) : this(version, PlatformResolver.Resolve(), RuntimeInformation.OSArchitecture) { }
        public Distribution(Version version, OSPlatform platform) : this(version, platform, RuntimeInformation.OSArchitecture) { }
        public Distribution(Version version, OSPlatform platform, Architecture arch)
        {
            Version = version;
            Platform = platform;
            BitSize = arch;
        }

        public Version Version { get; }
        public OSPlatform Platform { get; }
        public Architecture BitSize { get; }
    }
}
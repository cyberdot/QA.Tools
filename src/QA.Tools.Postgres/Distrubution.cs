using System.Runtime.InteropServices;

namespace QA.Tools.Postgres
{
    public class Distribution
    {
        public Versions Version { get; }

        public OSPlatform Platform { get; }

        public Architecture BitSize { get; }
    }
}
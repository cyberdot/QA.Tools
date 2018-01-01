using System.Collections.Generic;
using System.Linq;

namespace QA.Tools.Postgres
{
    public static class Versions
    {
        private static readonly List<Version> VersionsInternal = new List<Version>
        {
            new Version("9.1"),
            new Version("9.2"),
            new Version("9.3"),
            new Version("9.4"),
            new Version("10.0", isProduction: true)
        };


        public static Version Get(string version) => VersionsInternal.SingleOrDefault(v => v.SemVersion == version);
        public static Version Production => VersionsInternal.SingleOrDefault(v => v.IsProduction);
        public static int Count => VersionsInternal.Count;
    }
}
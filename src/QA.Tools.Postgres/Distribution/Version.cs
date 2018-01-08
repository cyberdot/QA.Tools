using Semver;

namespace QA.Tools.Postgres.Distribution
{
    public class Version
    {
        public SemVersion SemVersion { get; }
        public bool IsProduction { get; }

        public Version(string semVersion, bool isProduction = false)
        {
            SemVersion = semVersion;
            IsProduction = isProduction;
        }

        public override string ToString()
        {
            return SemVersion.ToString();
        }
    }
}
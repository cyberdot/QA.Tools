namespace QA.Tools.Postgres
{
    public class Version
    {
        public string SemVersion { get; }
        public bool IsProduction { get; }

        public Version(string semVersion, bool isProduction = false)
        {
            SemVersion = semVersion;
            IsProduction = isProduction;
        }
    }
}
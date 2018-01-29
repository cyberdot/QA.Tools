using System.Collections.Generic;
using QA.Tools.Postgres.Distribution;
using QA.Tools.Postgres.Utils;

namespace QA.Tools.Postgres.Config
{
    public class DefaultPgConfig : IPgConfig
    {
        private const string DefaultUser = "postgres";
        private const string DefaultPassword = "postgres";
        private const string DefaultDbName = "postgres";
        private const string DefaultDataDir = "data";

        private static readonly IReadOnlyCollection<string> DefaultParams = new List<string> { "-E", "UTF8"};

        public DefaultPgConfig() { }

        public DefaultPgConfig(Distribution.Distribution distribution, string dataDir)
        {
            Distribution = distribution;
            if (!string.IsNullOrWhiteSpace(dataDir))
            {
                DataDir = dataDir;
            }
        }

        public Distribution.Distribution Distribution { get; } = new Distribution.Distribution(Versions.Production);
        public int Port { get; } = Network.FindFreePort();
        public string DatabaseName => DefaultDbName;
        public string DataDir { get; } = DefaultDataDir;
        public string Username => DefaultUser;
        public string Password => DefaultPassword;
        public IReadOnlyCollection<string> AdditionalParams => DefaultParams;

        public string ToConnectionString()
        {
            return $"Server=localhost;Port={Port};" +
                   $"Database={DatabaseName};User Id={Username};" +
                   $"Password={Password};";
        }
    }
}
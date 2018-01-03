using System.Collections.Generic;

namespace QA.Tools.Postgres.Config
{
    public class DefaultPostgresConfig : IConfig
    {
        private const string DefaultUser = "postgres";
        private const string DefaultPassword = "postgres";
        private const string DefaultDbName = "postgres";
        private const string DefaultHost = "localhost";
        private static readonly Distribution Dist = new Distribution(Versions.Production);
        private static readonly IReadOnlyCollection<string> DefaultParams = new List<string> {
            "-E", "SQL_ASCII",
            "--locale=C",
            "--lc-collate=C",
            "--lc-ctype=C"};

        public Distribution Distribution => Dist;
        public string Host => DefaultHost;
        public int Port => 123;
        public string DatabaseName => DefaultDbName;
        public string DataDir => "";
        public string Username => DefaultUser;
        public string Password => DefaultPassword;
        public IReadOnlyCollection<string> AdditionalParams => DefaultParams;

        public string ToConnectionString()
        {
            return $"Server={Host};Port={Port};" +
                   $"Database={DatabaseName};User Id={Username};" +
                   $"Password={Password};";
        }
    }
}
using System.Collections.Generic;
using static QA.Tools.Postgres.Network;

namespace QA.Tools.Postgres
{
    public class EmbeddedPostgres
    {
        private readonly Versions version;
        private readonly string dataDir;

        public const string DefaultUser = "postgres";
        public const string DefaultPassword = "postgres";
        public const string DefaultDbName = "postgres";
        public const string DefaultHost = "localhost";
        private static readonly IReadOnlyCollection<string> DefaultParams = new List<string> {
            "-E", "SQL_ASCII",
            "--locale=C",
            "--lc-collate=C",
            "--lc-ctype=C"};

        public EmbeddedPostgres() : this(Versions.Production) { }

        public EmbeddedPostgres(Versions version) : this(version, null) { }

        public EmbeddedPostgres(Versions version, string dataDir)
        {
            this.version = version;
            this.dataDir = dataDir;
        }

        public string Start()
        {
            return Start(DefaultHost, FindFreePort(), DefaultDbName);
        }

        public string Start(string host, int port, string dbName)
        {
            return Start(host, port, dbName, DefaultUser, DefaultPassword, DefaultParams);
        }

        public string Start(string host, int port, string dbName, string user, string password)
        {
            return Start(DefaultRuntimeConfig(), host, port, dbName, user, password, DefaultParams);
        }

        public string Start(string host, int port, string dbName, string user, string password, List<string> additionalParams)
        {
            return Start(DefaultRuntimeConfig(), host, port, dbName, user, password, additionalParams);
        }

        public string Start(IRuntimeConfig runtimeConfig)
        {
            return Start(runtimeConfig, DefaultHost, FindFreePort(), DefaultDbName, DefaultUser, DefaultPassword, DefaultParams);
        }

        public string Start(IRuntimeConfig runtimeConfig, string host, int port, string dbName, string user, string password,
            List<string> additionalParams)
        {
           PostgresStarter<PostgresExecutable, PostgresProcess> runtime = PostgresStarter.getInstance(runtimeConfig);
                    var config = new PostgresConfig(version,
        new AbstractPostgresConfig.Net(host, port),
        new AbstractPostgresConfig.Storage(dbName, dataDir),
        new AbstractPostgresConfig.Timeout(),
        new AbstractPostgresConfig.Credentials(user, password)
        );
        config.getAdditionalInitDbParams().addAll(additionalParams);
        PostgresExecutable exec = runtime.prepare(config);
            this.process = exec.start();
        return FormatConnUrl(config);
    }


        public string FormatConnUrl(PostgresConfig config)
        {
            return $"Server={config.Host};Port={config.Port};" +
                   $"Database={config.DatabaseName};User Id={config.Username};" +
                   $"Password={config.Password};";
        }


}
}
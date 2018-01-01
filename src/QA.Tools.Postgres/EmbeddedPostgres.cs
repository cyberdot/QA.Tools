using System.Collections.Generic;
using QA.Tools.Postgres.Config;
using static QA.Tools.Postgres.Utils.Network;

namespace QA.Tools.Postgres
{
    public class EmbeddedPostgres
    {
        private readonly Distribution distribution;
        private readonly string dataDir;
        private static readonly IConfig DefaultConfig = new DefaultPostgresConfig();
        private static readonly IRuntimeConfig DefaultRuntimeConfig = new DefaultRuntimeConfig();

        public EmbeddedPostgres() : this(Versions.Production) { }

        public EmbeddedPostgres(Version version, string dataDir = null) : this(new Distribution(version), dataDir) {}

        public EmbeddedPostgres(Distribution dist, string dataDir = null)
        {
            distribution = dist;
            this.dataDir = dataDir;
        }

        public string Start()
        {
            return Start(DefaultConfig.Host, FindFreePort(), DefaultConfig.DatabaseName);
        }

        public string Start(string host, int port, string dbName)
        {
            return Start(host, port, dbName,
                DefaultConfig.Username, DefaultConfig.Password, 
                DefaultConfig.AdditionalParams);
        }

        public string Start(string host, int port, string dbName, string user, string password)
        {
            return Start(DefaultRuntimeConfig, host, port, 
                dbName, user, password, DefaultConfig
                .AdditionalParams);
        }

        public string Start(string host, int port, string dbName, 
            string user, string password, IReadOnlyCollection<string> additionalParams)
        {
            return Start(DefaultRuntimeConfig, host, port, 
                dbName, user, password, 
                additionalParams);
        }

        public string Start(IRuntimeConfig runtimeConfig)
        {
            return Start(runtimeConfig, DefaultConfig.Host, 
                FindFreePort(), 
                DefaultConfig.DatabaseName, 
                DefaultConfig.Username, DefaultConfig.Password, 
                DefaultConfig.AdditionalParams);
        }

        public string Start(IRuntimeConfig runtimeConfig,
            string host, int port, string dbName,
            string user, string password,
            IReadOnlyCollection<string> additionalParams)
        {
            var config = new PostgresConfig(distribution,
                host, port,
                dbName, dataDir,
                user, password, additionalParams);

            var process = new PostgresProcess(runtimeConfig, config);
            process.Start();

            return config.ToConnectionString();
        }

    }
}
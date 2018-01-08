using System;
using System.Collections.Generic;
using QA.Tools.Postgres.Commands;
using QA.Tools.Postgres.Config;
using QA.Tools.Postgres.Distribution;
using QA.Tools.Postgres.Process;
using static QA.Tools.Postgres.Utils.Network;
using Version = QA.Tools.Postgres.Distribution.Version;

namespace QA.Tools.Postgres
{
    public class EmbeddedPostgres : IDisposable
    {
        private readonly IPgConfig config;
        private static readonly IRuntimeConfig DefaultRuntimeConfig = new DefaultRuntimeConfig();
        private PostgresProcess process;

        public EmbeddedPostgres() : this(Versions.Production) { }
        public EmbeddedPostgres(Version version, string dataDir = null) : this(new Distribution.Distribution(version), dataDir) {}
        public EmbeddedPostgres(Distribution.Distribution dist, string dataDir = null)
        {
            config = new DefaultPgConfig(dist, dataDir);
        }

        public string Start()
        {
            return Start(FindFreePort(), config.DatabaseName);
        }
        public string Start(int port, string dbName)
        {
            return Start(port, dbName,
                config.Username, config.Password, 
                config.AdditionalParams);
        }
        public string Start(int port, string dbName, string user, string password)
        {
            return Start(DefaultRuntimeConfig, port, 
                dbName, user, password, config
                .AdditionalParams);
        }
        public string Start(int port, string dbName, 
            string user, string password, IReadOnlyCollection<string> additionalParams)
        {
            return Start(DefaultRuntimeConfig, port, 
                dbName, user, password, 
                additionalParams);
        }
        public string Start(IRuntimeConfig runtimeConfig)
        {
            return Start(runtimeConfig,  
                FindFreePort(), 
                config.DatabaseName, 
                config.Username, config.Password, 
                config.AdditionalParams);
        }
        public string Start(IRuntimeConfig runtimeConfig,
            int port, string dbName,
            string user, string password,
            IReadOnlyCollection<string> additionalParams)
        {
            var newConfig = new PostgresConfig(config.Distribution,
                port,
                dbName, config.DataDir,
                user, password, additionalParams);

            process = new PostgresProcess(newConfig, runtimeConfig);
            process.Start().Wait();

            return newConfig.ToConnectionString();
        }

        public void Stop()
        {
            if (process != null)
            {
                var path = process.DistributionPackage.FileSet.Executable.FullName;
                var stopCmd = new StopServerCommand();
                stopCmd.Run(path, config.DataDir, config.AdditionalParams);

                process.Stop();
            }
        }

        public void Dispose() { Stop(); }
    }
}
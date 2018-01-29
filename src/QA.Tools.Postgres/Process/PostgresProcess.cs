using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QA.Tools.Postgres.Commands;
using QA.Tools.Postgres.Config;
using QA.Tools.Postgres.Distribution;

namespace QA.Tools.Postgres.Process
{
    public class PostgresProcess
    {
        private readonly IPgConfig config;
        private readonly IRuntimeConfig runtimeConfig;

        public PostgresProcess(IPgConfig config, IRuntimeConfig runtimeConfig)
        {
            this.runtimeConfig = runtimeConfig;
            this.config = config;
        }

        public DistributionPackage DistributionPackage { get; private set; }

        public void Start()
        {
            var store = runtimeConfig.ArtifactStore;
            var distribution = config.Distribution;

            DistributionPackage = store.GetDistributionPackage(distribution);
            if (DistributionPackage?.BinTools != null)
            {
                ExecuteCommands();
            }
        }

        private void ExecuteCommands()
        {
            var binTools = DistributionPackage.BinTools;
            var dataDir = Path.Combine(DistributionPackage.InstallationPath.FullName, config.DataDir);

            var initOpts = config.AdditionalParams.Union(new List<string>
            {
                "-U", config.Username,
                "-A", "trust"
            }).ToList();
            var startOpts = new List<string>
            {
                "-p", config.Port.ToString(),
                "-F"
            };

            foreach (var cmd in runtimeConfig.Commands)
            {
                var opts = cmd is InitDbCommand ? initOpts : startOpts;
                cmd.Run(binTools, dataDir, opts);
            }

            var checkReadiness = new IsDbReadyCommand();
            var isReadyOpts = new List<string>
            {
                "-p", config.Port.ToString(),
                "-U", config.Username
            };
            for (var i = 0; i < 3; i++)
            {
                var status = checkReadiness.Run(binTools, config.DataDir, isReadyOpts);
                if (status) return;

                Task.Delay(TimeSpan.FromMilliseconds(500))
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        public void Stop()
        {
            var cmd = (StartServerCommand) runtimeConfig.Commands.SingleOrDefault(c => c.GetType() == typeof(StartServerCommand));
            cmd?.Process.Kill();
        }
    }
}
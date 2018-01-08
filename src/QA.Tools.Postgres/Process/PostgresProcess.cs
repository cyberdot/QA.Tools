using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QA.Tools.Postgres.Commands;
using QA.Tools.Postgres.Config;
using QA.Tools.Postgres.Distribution;
using QA.Tools.Postgres.Store;

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

        public async Task Start()
        {
            var store = runtimeConfig.ArtifactStore;
            var distribution = config.Distribution;

            DistributionPackage = await store.GetDistributionPackage(distribution)
                .ConfigureAwait(false);
            if (DistributionPackage?.FileSet != null)
            {
                ExecuteCommands(DistributionPackage.FileSet);
            }
        }

        private void ExecuteCommands(ExtractedFileSet fileSet)
        {
            var exePath = fileSet.Executable.FullName;
            var dataDir = Path.Combine(fileSet.Executable.Directory.Parent.FullName, config.DataDir);

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
                cmd.Run(exePath, dataDir, opts);
            }
        }

        public void Stop()
        {
            var cmd = (StartServerCommand) runtimeConfig.Commands.SingleOrDefault(c => c.GetType() == typeof(StartServerCommand));
            cmd?.Process.Kill();
        }
    }
}
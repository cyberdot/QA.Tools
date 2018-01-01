using System.IO;
using QA.Tools.Postgres.Config;

namespace QA.Tools.Postgres
{
    public class PostgresProcess
    {
        private readonly IRuntimeConfig runtimeConfig;
        private readonly IConfig config;

        public PostgresProcess(IRuntimeConfig runtimeConfig, IConfig config)
        {
            this.runtimeConfig = runtimeConfig;
            this.config = config;
        }

        public void Start()
        {
            var store = runtimeConfig.ArtifactStore;
            var distribution = config.Distribution;

            if (store.CheckDistribution(distribution))
            {
                var fileSet = store.ExtractFileSet(distribution);
                StartExecutable(fileSet.Executable);
            }
        }

        private void StartExecutable(FileInfo exeFile)
        {

        }
    }
}
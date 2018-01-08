using System.Collections.Generic;
using QA.Tools.Postgres.Commands;
using QA.Tools.Postgres.Store;

namespace QA.Tools.Postgres.Config
{
    public class DefaultRuntimeConfig : IRuntimeConfig
    {
        public DefaultRuntimeConfig()
        {
            ArtifactStore = new LocalUserStore(new DefaultStoreConfig());
            Commands = new List<ICommand> { new InitDbCommand(), new StartServerCommand() };
        }

        public IArtifactStore ArtifactStore { get; }
        public IEnumerable<ICommand> Commands { get; }
    }
}
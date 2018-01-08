using System.Collections.Generic;
using QA.Tools.Postgres.Commands;
using QA.Tools.Postgres.Store;

namespace QA.Tools.Postgres.Config
{
    public interface IRuntimeConfig
    {
        IArtifactStore ArtifactStore { get; }
        IEnumerable<ICommand> Commands { get; }
    }
}
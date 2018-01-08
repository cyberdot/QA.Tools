using System.IO;
using QA.Tools.Postgres.Resolver;

namespace QA.Tools.Postgres.Config
{
    public interface IStoreConfig
    {
        string PackagesCache { get; }
        string DistributionsRoot { get; }
        DirectoryInfo ArtifactStorePath { get; }

        IPackageResolver PackageResolver { get; }
    }
}
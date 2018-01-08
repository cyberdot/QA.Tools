using System;
using System.IO;
using System.Threading.Tasks;
using QA.Tools.Postgres.Config;
using QA.Tools.Postgres.Distribution;

namespace QA.Tools.Postgres.Store
{
    public class LocalUserStore : IArtifactStore
    {
        private readonly IStoreConfig config;
        private readonly DirectoryInfo storePath;

        public LocalUserStore(IStoreConfig config)
        {
            this.config = config;
            storePath = this.config.ArtifactStorePath;
        }

        public async Task<DistributionPackage> GetDistributionPackage(Distribution.Distribution distribution)
        {
            var artifactStorePath = storePath;
            var packageResolver = config.PackageResolver;

            if (!artifactStorePath.Exists)
            {
                artifactStorePath.Create();
                artifactStorePath.CreateSubdirectory(config.PackagesCache);
                artifactStorePath.CreateSubdirectory(config.DistributionsRoot);
            }

            var distroPath = GenerateDistroPath(distribution);
            return await packageResolver.Setup(distribution, distroPath)
                .ConfigureAwait(false);
        }

        private DirectoryInfo GenerateDistroPath(Distribution.Distribution distribution)
        {
            var newDistroDirectory = $"{Guid.NewGuid()}-{distribution.Version}-{distribution.BitSize.ToString()}"
                .ToLowerInvariant();

            var path = new DirectoryInfo(Path.Combine(storePath.FullName, config.DistributionsRoot, newDistroDirectory));
            if (!path.Exists) { path.Create(); }
            return path;
        }
    }
}
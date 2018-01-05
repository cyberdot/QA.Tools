using System.IO;
using QA.Tools.Postgres.Config;

namespace QA.Tools.Postgres.Store
{
    public class LocalUserStore : IArtifactStore
    {
        private readonly IArtifactStoreConfig config;

        public LocalUserStore(IArtifactStoreConfig config)
        {
            this.config = config;
        }

        public bool CheckDistribution(Distribution distribution)
        {
            var artifactStorePath = config.ArtifactStorePath;
            var packageResolver = config.PackageResolver;

            if (!artifactStorePath.Exists)
            {
                artifactStorePath.Create();
            }

            var distroPath = new DirectoryInfo(Path.Combine(artifactStorePath.FullName,
                distribution.Version.SemVersion, distribution.BitSize.ToString()));

            if (distroPath.Exists)
            {
                distroPath.Delete(true);
                distroPath.Create();
            }
            
            packageResolver.Setup(distribution, distroPath);
            return true;
        }

        public ExtractedFileSet ExtractFileSet(Distribution distribution)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveFileSet(Distribution distribution, ExtractedFileSet files)
        {
            throw new System.NotImplementedException();
        }
    }
}
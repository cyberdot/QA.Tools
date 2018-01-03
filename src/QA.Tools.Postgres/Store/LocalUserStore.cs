using System.IO;
using QA.Tools.Postgres.Config;

namespace QA.Tools.Postgres.Store
{
    public class LocalUserStore : IArtifactStore
    {
        private readonly IArtifactConfig config;

        public LocalUserStore(IArtifactConfig config)
        {
            this.config = config;
        }

        public bool CheckDistribution(Distribution distribution)
        {
            if (!config.ArtifactStorePath.Exists)
            {
                config.ArtifactStorePath.Create();
            }

            //check if distro exists already
            //if not download and extract it
            //return true

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
namespace QA.Tools.Postgres.Store
{
    public class LocalUserStore : IArtifactStore
    {
        public bool CheckDistribution(Distribution distribution)
        {
            throw new System.NotImplementedException();
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
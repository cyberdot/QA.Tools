namespace QA.Tools.Postgres.Store
{
    public interface IArtifactStore
    {
        bool CheckDistribution(Distribution distribution);

        ExtractedFileSet ExtractFileSet(Distribution distribution);

        void RemoveFileSet(Distribution distribution, ExtractedFileSet files);
    }
}
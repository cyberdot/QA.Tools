using QA.Tools.Postgres.Distribution;

namespace QA.Tools.Postgres.Store
{
    public interface IArtifactStore
    {
        DistributionPackage GetDistributionPackage(Distribution.Distribution distribution);
    }
}
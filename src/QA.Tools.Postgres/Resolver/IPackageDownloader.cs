using QA.Tools.Postgres.Distribution;

namespace QA.Tools.Postgres.Resolver
{
    public interface IPackageDownloader
    {
        DistributionPackage GetPackage(Distribution.Distribution distribution);
    }
}
using System.Threading.Tasks;
using QA.Tools.Postgres.Distribution;

namespace QA.Tools.Postgres.Resolver
{
    public interface IPackageDownloader
    {
        Task<DistributionPackage> GetPackageAsync(Distribution.Distribution distribution);
    }
}
using System.Threading.Tasks;

namespace QA.Tools.Postgres
{
    public interface IPackageDownloader
    {
        Task<DistributionPackage> GetPackageAsync(Distribution distribution);
    }
}
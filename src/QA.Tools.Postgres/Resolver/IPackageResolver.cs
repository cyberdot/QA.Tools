using System.IO;
using System.Threading.Tasks;
using QA.Tools.Postgres.Distribution;

namespace QA.Tools.Postgres.Resolver
{
    public interface IPackageResolver
    {
        Task<DistributionPackage> Setup(Distribution.Distribution distribution, DirectoryInfo distroPath);
    }
}
using System.IO;
using QA.Tools.Postgres.Distribution;

namespace QA.Tools.Postgres.Resolver
{
    public interface IPackageResolver
    {
        DistributionPackage Setup(Distribution.Distribution distribution, DirectoryInfo distroPath);
    }
}
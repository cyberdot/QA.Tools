using System.IO;
using QA.Tools.Postgres.Distribution;

namespace QA.Tools.Postgres.Resolver
{
    public class DefaultPackageResolver : IPackageResolver
    {
        private readonly IPackageDownloader downloader;

        public DefaultPackageResolver(IPackageDownloader downloader)
        {
            this.downloader = downloader;
        }

        public DistributionPackage Setup(Distribution.Distribution distribution, DirectoryInfo distroPath)
        {
            var package = downloader.GetPackage(distribution);
            package.CopyTo(distroPath);

            distroPath.Refresh();
            return package;
        }
    }
}
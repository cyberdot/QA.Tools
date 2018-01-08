using System.IO;
using System.Threading.Tasks;
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

        public async Task<DistributionPackage> Setup(Distribution.Distribution distribution, DirectoryInfo distroPath)
        {
            var package = await downloader
                .GetPackageAsync(distribution)
                .ConfigureAwait(false);

            package.CopyTo(distroPath);
            package.ExtractFileSet(distroPath);

            distroPath.Refresh();
            return package;
        }
    }
}
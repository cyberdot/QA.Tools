using System.IO;
using System.Threading.Tasks;

namespace QA.Tools.Postgres
{
    public class DefaultPackageResolver : IPackageResolver
    {
        private readonly IPackageDownloader downloader;

        public DefaultPackageResolver(IPackageDownloader downloader)
        {
            this.downloader = downloader;
        }

        public async Task Setup(Distribution distribution, DirectoryInfo distroPath)
        {
            var package = await downloader.GetPackageAsync(distribution);

            package.CopyTo(distroPath);
        }
    }
}
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QA.Tools.Postgres
{
    public class PostgresPackageDownloader : IPackageDownloader
    {
        private readonly IDownloaderConfig config;

        public PostgresPackageDownloader(IDownloaderConfig config)
        {
            this.config = config;
        }

        public async Task<DistributionPackage> GetPackageAsync(Distribution distribution)
        {
            var distroPath = GetFileName(distribution);
            if (File.Exists(distroPath))
            {
                return new DistributionPackage(distroPath);
            }

            return await DownloadPackageAsync(distribution);
        }

        private async Task<DistributionPackage> DownloadPackageAsync(Distribution distribution)
        {
            var distroPath = GetFileName(distribution);

            var client = new HttpClient { BaseAddress = config.DownloadUri };
            var response = await client.GetAsync(GenerateUrl(distribution));
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                await stream.CopyToAsync(File.Create(distroPath));
            }
            return new DistributionPackage(distroPath);
        }

        private string GenerateUrl(Distribution distribution)
        {
            var fileExt = distribution.Platform == OSPlatform.Linux ? "tar.gz" : "zip";

            var urlBuilder = new StringBuilder("postgresql-", 1024);
            urlBuilder.Append(distribution.Version.SemVersion);
            urlBuilder.Append("-2-");
            urlBuilder.Append(distribution.Platform.ToString().ToLowerInvariant());
            urlBuilder.Append("-binaries.");
            urlBuilder.Append(fileExt);

            return urlBuilder.ToString();
        }

        private string GetFileName(Distribution distribution)
        {
            return Path.Combine(
                config.PackagesCachePath.FullName, 
                $"{distribution.Version.SemVersion}-{distribution.BitSize.ToString()}");
        }
    }
}
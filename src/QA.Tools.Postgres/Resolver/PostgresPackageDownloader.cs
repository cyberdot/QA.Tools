using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using QA.Tools.Postgres.Distribution;
using Semver;

namespace QA.Tools.Postgres.Resolver
{
    public class PostgresPackageDownloader : IPackageDownloader
    {
        private readonly IDownloaderConfig config;

        public PostgresPackageDownloader(IDownloaderConfig config)
        {
            this.config = config;
        }

        public async Task<DistributionPackage> GetPackageAsync(Distribution.Distribution distribution)
        {
            var distroPath = GetFileName(distribution);
            if (File.Exists(distroPath))
            {
                return new DistributionPackage(distroPath);
            }

            return await DownloadPackageAsync(distribution);
        }

        private async Task<DistributionPackage> DownloadPackageAsync(Distribution.Distribution distribution)
        {
            var distroPath = GetFileName(distribution);
            var distroDownloadUrl = GenerateUrl(distribution);

            var client = new HttpClient { BaseAddress = config.DownloadUri };
            client.DefaultRequestHeaders.Add("User-Agent", config.UserAgent);
            var response = await client.GetAsync(distroDownloadUrl, HttpCompletionOption.ResponseContentRead);

            if(!response.IsSuccessStatusCode) throw new ArgumentException("The specified distribution cannot be downloaded");

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                await stream.CopyToAsync(File.Create(distroPath));
            }
            return new DistributionPackage(distroPath);
        }

        private static Uri GenerateUrl(Distribution.Distribution distribution)
        {
            var fileExt = distribution.Platform == OSPlatform.Linux ? "tar.gz" : "zip";

            var urlBuilder = new StringBuilder("postgresql-", 1024);
            urlBuilder.Append(distribution.Version.SemVersion);
            urlBuilder.Append(distribution.Version.SemVersion < new SemVersion(9, 6, 6) ? "-2-" : "-3-");
            urlBuilder.Append(distribution.Platform.ToString().ToLowerInvariant());

            if (distribution.BitSize == Architecture.X64)
            {
                urlBuilder.Append("-x64");
            }

            urlBuilder.Append("-binaries.");
            urlBuilder.Append(fileExt);

            return new Uri(urlBuilder.ToString(), UriKind.Relative);
        }

        private string GetFileName(Distribution.Distribution distribution)
        {
            var fileExt = distribution.Platform == OSPlatform.Linux ? "tar.gz" : "zip";
            return Path.Combine(
                config.PackagesCachePath.FullName, 
                $"{distribution.Version.SemVersion}-{distribution.BitSize.ToString()}.{fileExt}".ToLowerInvariant());
        }
    }
}
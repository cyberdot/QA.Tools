using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using QA.Tools.Postgres.Config;
using QA.Tools.Postgres.Distribution;
using QA.Tools.Postgres.Resolver;
using Xunit;

namespace QA.Tools.Postgres.Tests
{
    public class PostgresPackageDownloaderTests
    {
        [Fact]
        public async Task Should_download_postgres_package()
        {
            var config = new DefaultStoreConfig();
            var cacheDir = new DirectoryInfo(Path.Combine(config.ArtifactStorePath.FullName, config.PackagesCache));
            var downloader = new PostgresPackageDownloader(new PostgresDownloaderConfig(cacheDir));
            var distro = new Distribution.Distribution(new Version("9.3.20"), OSPlatform.Windows, Architecture.X64);

            var package = await downloader.GetPackageAsync(distro);

            Assert.NotNull(package);
        }
    }
}
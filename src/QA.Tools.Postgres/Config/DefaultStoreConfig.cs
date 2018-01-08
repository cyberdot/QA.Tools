using System;
using System.IO;
using QA.Tools.Postgres.Resolver;

namespace QA.Tools.Postgres.Config
{
    public class DefaultStoreConfig : IStoreConfig
    {
        private const string PackagesCacheDirectory = "packages";
        private const string DistributionsRootDirectory = "distributions";
        private const string ArtifactStoreRoot = ".embedpostgresql";

        private static readonly DirectoryInfo StorePath = new DirectoryInfo(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), 
            ArtifactStoreRoot));

        public DefaultStoreConfig()
        {
            var packagesCacheDir = new DirectoryInfo(Path.Combine(StorePath.FullName, PackagesCacheDirectory));
            var config = new PostgresDownloaderConfig(packagesCacheDir);
            var downloader = new PostgresPackageDownloader(config);
            PackageResolver = new DefaultPackageResolver(downloader);
        }

        public string PackagesCache => PackagesCacheDirectory;
        public string DistributionsRoot => DistributionsRootDirectory;
        public DirectoryInfo ArtifactStorePath => StorePath;
        public IPackageResolver PackageResolver { get; }
    }
}
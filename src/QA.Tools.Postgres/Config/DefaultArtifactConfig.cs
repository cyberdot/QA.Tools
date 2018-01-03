using System;
using System.IO;

namespace QA.Tools.Postgres.Config
{
    public class DefaultArtifactConfig : IArtifactConfig
    {
        private const string ArtifactStoreRoot = ".embedpostgresql";
        private const string DPrefix = "postgresql-download";
        private static readonly Uri DownloadUri = new Uri("http://get.enterprisedb.com/postgresql/");
        private static readonly DirectoryInfo StorePath = new DirectoryInfo(
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ArtifactStoreRoot));
        private const string UserAgentString = "Mozilla/5.0 (compatible; Embedded postgres; +https://github.com/cyberdot/qa.tools)";


        public DefaultArtifactConfig()
        {
//            PackageResolver = new PostgresPackageResolver();
        }

        public Uri DownloadPath => DownloadUri;
        public DirectoryInfo ArtifactStorePath => StorePath;
        public string DownloadPrefix => DPrefix;
        public string UserAgent => UserAgentString;
        public IPackageResolver PackageResolver { get; }
    }
}
using System;
using System.IO;
using QA.Tools.Postgres.Resolver;

namespace QA.Tools.Postgres.Config
{
    public class PostgresDownloaderConfig : IDownloaderConfig
    {
        public PostgresDownloaderConfig(DirectoryInfo packagesCache)
        {
            PackagesCachePath = packagesCache;
        }

        public DirectoryInfo PackagesCachePath { get; }

        private const string UserAgentString = "Mozilla/5.0 (compatible; Embedded postgres; +https://github.com/cyberdot/qa.tools)";
        public Uri DownloadUri => new Uri("https://get.enterprisedb.com/postgresql/");
        public string UserAgent => UserAgentString;
    }
}
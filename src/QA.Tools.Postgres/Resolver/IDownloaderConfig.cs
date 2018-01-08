using System;
using System.IO;

namespace QA.Tools.Postgres.Resolver
{
    public interface IDownloaderConfig
    {
        DirectoryInfo PackagesCachePath { get; }
        Uri DownloadUri { get; }
        string UserAgent { get; }
    }
}
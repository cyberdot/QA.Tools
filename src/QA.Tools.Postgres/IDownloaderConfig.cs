using System;
using System.IO;

namespace QA.Tools.Postgres
{
    public interface IDownloaderConfig
    {
        DirectoryInfo PackagesCachePath { get; }
        Uri DownloadUri { get; }
    }
}
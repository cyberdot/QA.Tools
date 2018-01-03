using System;
using System.IO;

namespace QA.Tools.Postgres.Config
{
    public interface IArtifactConfig
    {
        Uri DownloadPath { get; }

        DirectoryInfo ArtifactStorePath { get; }

        string DownloadPrefix { get; }

        string UserAgent { get; }

        IPackageResolver PackageResolver { get; }
    }
}
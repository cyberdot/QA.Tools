using System.Collections.Generic;
using System.IO;
using System.Linq;
using SharpCompress.Readers;

namespace QA.Tools.Postgres.Distribution
{
    public class DistributionPackage
    {
        private const string SearchPattern = "pgsql\\bin\\*.*";
        private readonly string distroPath;

        public DistributionPackage() { }
        public DistributionPackage(string distroPath)
        {
            this.distroPath = distroPath;
        }

        public IReadOnlyCollection<FileInfo> BinTools { get; private set; }
        public DirectoryInfo InstallationPath { get; private set; }

        public void CopyTo(DirectoryInfo path)
        {
            if (!path.EnumerateFiles().Any())
            {
                using (var reader = ReaderFactory.Open(File.OpenRead(distroPath),
                    new ReaderOptions {LookForHeader = true}))
                {
                    while (reader.MoveToNextEntry())
                    {
                        reader.WriteEntryToDirectory(
                            path.FullName, 
                            new ExtractionOptions{ Overwrite = true, ExtractFullPath = true});
                    }
                }
            }
            InstallationPath = path;
            BinTools = path.GetFiles(SearchPattern);
        }
    }
}
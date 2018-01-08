using System.IO;
using System.Linq;
using QA.Tools.Postgres.Store;
using SharpCompress.Readers;

namespace QA.Tools.Postgres.Distribution
{
    public class DistributionPackage
    {
        private const string SearchPattern = "pgsql\\bin\\*.*";
        private const string PgCtlExe = "pg_ctl";


        private readonly string distroPath;
        public DistributionPackage() { }

        public DistributionPackage(string distroPath)
        {
            this.distroPath = distroPath;
        }

        public ExtractedFileSet FileSet { get; private set; }
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
        }

        public void ExtractFileSet(DirectoryInfo path)
        {
            var files = path.GetFiles(SearchPattern);

            FileSet = new ExtractedFileSet(
                files.SingleOrDefault(f => f.Name.Contains(PgCtlExe)),
                files.Where(f => f.Name.Contains(PgCtlExe) != true).ToList());
        }
    }
}
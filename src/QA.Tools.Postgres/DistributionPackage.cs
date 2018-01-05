using System.IO;
using SharpCompress.Readers;

namespace QA.Tools.Postgres
{
    public class DistributionPackage
    {
        private readonly string distroPath;
        public DistributionPackage() { }

        public DistributionPackage(string distroPath)
        {
            this.distroPath = distroPath;
        }

        public void CopyTo(DirectoryInfo path)
        {
            using (var reader = ReaderFactory.Open(File.OpenRead(distroPath), new ReaderOptions { LookForHeader = true }))
            {
                reader.WriteAllToDirectory(path.FullName, new ExtractionOptions { Overwrite = true });
            }
        }
    }
}
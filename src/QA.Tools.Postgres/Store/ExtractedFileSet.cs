using System.Collections.Generic;
using System.IO;

namespace QA.Tools.Postgres.Store
{
    public class ExtractedFileSet
    {
        public ExtractedFileSet(FileInfo exe, IReadOnlyCollection<FileInfo> libs)
        {
            Executable = exe;
            LibraryFiles = libs;
        }

        public FileInfo Executable { get; }
        public IReadOnlyCollection<FileInfo> LibraryFiles { get; }
    }
}
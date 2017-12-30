﻿using System.Collections.Generic;
using System.IO;

namespace QA.Tools.Postgres
{
    public class ExtractedFileSet
    {
        public FileInfo Executable { get; }
        public IReadOnlyCollection<FileInfo> LibraryFiles { get; }
    }
}
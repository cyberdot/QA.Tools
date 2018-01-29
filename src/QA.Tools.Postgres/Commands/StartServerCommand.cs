﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace QA.Tools.Postgres.Commands
{
    public class StartServerCommand : ICommand
    {
        public System.Diagnostics.Process Process { get; private set; }

        public void Run(IReadOnlyCollection<FileInfo> binTools, string dataDir, IReadOnlyCollection<string> additionalOptions)
        {
            var exePath = binTools.SingleOrDefault(f => f.Name.Contains("pg_ctl"));
            if(exePath == null) throw new ArgumentException("Cannot find pg_ctl binary");

            Process = new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    FileName = exePath.FullName,
                    Arguments = $"-s -D {dataDir} -o \"{string.Join(" ", additionalOptions)}\" start"
                }
            };
            Process.Start();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace QA.Tools.Postgres.Commands
{
    public class StopServerCommand : ICommand
    {
        public void Run(IReadOnlyCollection<FileInfo> binTools, string dataDir, IReadOnlyCollection<string> options)
        {
            var exePath = binTools.SingleOrDefault(f => f.Name.Contains("pg_ctl"));
            if (exePath == null) throw new ArgumentException("Cannot find pg_ctl binary");

            var process = new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    FileName = exePath.FullName,
                    Arguments = $"-w -s -D {dataDir} -m s stop"
                }
            };
            process.Start();
        }
    }
}
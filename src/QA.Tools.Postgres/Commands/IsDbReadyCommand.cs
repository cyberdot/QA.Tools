using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace QA.Tools.Postgres.Commands
{
    public class IsDbReadyCommand : ICommand<bool>
    {
        public bool Run(IReadOnlyCollection<FileInfo> binTools, string dataDir, IReadOnlyCollection<string> options)
        {
            var exePath = binTools.SingleOrDefault(f => f.Name.Contains("pg_isready"));
            if(exePath == null) throw new ArgumentException("Cannot find pg_isready binary");

            var process = new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = true,
                    FileName = exePath.FullName,
                    RedirectStandardOutput = true,
                    Arguments = $"{string.Join(" ", options)}"
                }
            };
            process.Start();

            var output = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();

            return output.Contains("accepting connections");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace QA.Tools.Postgres.Commands
{
    public class InitDbCommand : ICommand
    {
        public void Run(IReadOnlyCollection<FileInfo> binTools, string dataDir, IReadOnlyCollection<string> additionalOptions)
        {
            var exePath = binTools.SingleOrDefault(f => f.Name.Contains("pg_ctl"));
            if(exePath == null) throw new ArgumentException("Cannot find pg_ctl binary");

            var process = new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = true,
                    FileName = exePath.FullName,
                    Arguments = $"-s -D {dataDir} -o \"{string.Join(" ", additionalOptions)}\" init"
                }
            };
            process.Start();

            process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode > 0)
            {
                throw new ArgumentException("Failure initialising database");
            }
        }
    }
}
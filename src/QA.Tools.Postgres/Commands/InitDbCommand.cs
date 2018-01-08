using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QA.Tools.Postgres.Commands
{
    public class InitDbCommand : ICommand
    {
        public void Run(string exePath, string dataDir, IReadOnlyCollection<string> additionalOptions)
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = true, FileName = exePath,
                    Arguments = $"-s -D {dataDir} -o \"{string.Join(" ", additionalOptions)}\" init"
                }
            };
            process.Start();
            process.WaitForExit();

            if (process.ExitCode > 0)
            {
                throw new ArgumentException();
            }
        }
    }
}
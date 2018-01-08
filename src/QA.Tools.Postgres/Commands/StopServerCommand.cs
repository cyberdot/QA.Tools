using System.Collections.Generic;
using System.Diagnostics;

namespace QA.Tools.Postgres.Commands
{
    public class StopServerCommand : ICommand
    {
        public void Run(string exePath, string dataDir, IReadOnlyCollection<string> options)
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    FileName = exePath,
                    Arguments = $"-w -s -D {dataDir} -m s stop"
                }
            };
            process.Start();
        }
    }
}
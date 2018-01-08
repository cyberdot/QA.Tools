using System.Collections.Generic;
using System.Diagnostics;

namespace QA.Tools.Postgres.Commands
{
    public class StartServerCommand : ICommand
    {
        public System.Diagnostics.Process Process { get; private set; }

        public void Run(string exePath, string dataDir, IReadOnlyCollection<string> additionalOptions)
        {
            Process = new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    FileName = exePath,
                    Arguments = $"-s -D {dataDir} -o \"{string.Join(" ", additionalOptions)}\" start"
                }
            };
            Process.Start();
        }
    }
}
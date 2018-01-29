using System.Collections.Generic;
using System.IO;

namespace QA.Tools.Postgres.Commands
{
    public interface ICommand
    {
        void Run(IReadOnlyCollection<FileInfo> binTools, string dataDir, IReadOnlyCollection<string> options);
    }

    public interface ICommand<out T>
    {
        T Run(IReadOnlyCollection<FileInfo> binTools, string dataDir, IReadOnlyCollection<string> options);
    }
}
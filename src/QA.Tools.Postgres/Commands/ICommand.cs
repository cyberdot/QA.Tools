using System.Collections.Generic;

namespace QA.Tools.Postgres.Commands
{
    public interface ICommand
    {
        void Run(string exePath, string dataDir, IReadOnlyCollection<string> options);
    }

    public interface ICommand<out T>
    {
        T Run();
    }
}
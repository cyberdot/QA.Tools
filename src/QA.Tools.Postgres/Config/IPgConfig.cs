using System.Collections.Generic;

namespace QA.Tools.Postgres.Config
{
    public interface IPgConfig
    {
        Distribution.Distribution Distribution { get; }
        int Port { get; }
        string DatabaseName { get; }
        string DataDir { get; }
        string Username { get; }
        string Password { get; }
        IReadOnlyCollection<string> AdditionalParams { get; }
        string ToConnectionString();
    }
}
using System.Collections.Generic;

namespace QA.Tools.Postgres.Config
{
    public interface IConfig
    {
        Distribution Distribution { get; }
        string Host { get; }
        int Port { get; }
        string DatabaseName { get; }
        string DataDir { get; }
        string Username { get; }
        string Password { get; }
        IReadOnlyCollection<string> AdditionalParams { get; }
        string ToConnectionString();
    }
}
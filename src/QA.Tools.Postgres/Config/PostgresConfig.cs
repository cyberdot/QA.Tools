using System.Collections.Generic;

namespace QA.Tools.Postgres.Config
{
    public class PostgresConfig : IPgConfig
    {
        public Distribution.Distribution Distribution { get; }
        public int Port { get; }
        public string DatabaseName { get; }
        public string DataDir { get; }
        public string Username { get; }
        public string Password { get; }
        public IReadOnlyCollection<string> AdditionalParams { get; }

        public PostgresConfig(
            Distribution.Distribution distribution, 
            int port, 
            string dbName, string dataDir, 
            string user, string password, 
            IReadOnlyCollection<string> additionalParams)
        {
            Distribution = distribution;
            Port = port;
            DatabaseName = dbName;
            DataDir = dataDir;
            Username = user;
            Password = password;
            AdditionalParams = additionalParams;
        }

        public string ToConnectionString()
        {
            return $"Server=localhost;Port={Port};" +
                   $"Database={DatabaseName};User Id={Username};" +
                   $"Password={Password};";
        }
    }
}
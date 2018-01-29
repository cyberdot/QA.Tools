using System.Data;
using FluentAssertions;
using Npgsql;
using QA.Tools.Postgres.Config;
using QA.Tools.Postgres.Distribution;
using QA.Tools.Postgres.Process;
using Xunit;

namespace QA.Tools.Postgres.Tests
{
    public class PostgresProcessTests
    {
        [Fact]
        public void Should_start_postgres_process()
        {
            var runtimeConfig = new DefaultRuntimeConfig();
            var postgresConfig = new DefaultPgConfig(new Distribution.Distribution(new Version("9.3.20")), "data");
            var process = new PostgresProcess(postgresConfig, runtimeConfig);

            process.Start();
        }

        [Fact]
        public void Should_be_able_to_connect_to_db_using_db_provider()
        {
            var runtimeConfig = new DefaultRuntimeConfig();
            var postgresConfig = new DefaultPgConfig(new Distribution.Distribution(new Version("9.3.20")), "data");
            var process = new PostgresProcess(postgresConfig, runtimeConfig);

            process.Start();

            using (var conn = new NpgsqlConnection(postgresConfig.ToConnectionString()))
            {
                conn.Open();

                conn.State.Should().Be(ConnectionState.Open);
            }

        }
    }
}
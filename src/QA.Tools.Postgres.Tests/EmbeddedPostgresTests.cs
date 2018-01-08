using QA.Tools.Postgres.Distribution;
using Xunit;

namespace QA.Tools.Postgres.Tests
{
    public class EmbeddedPostgresTests
    {
        [Fact]
        public void Should_start_server_instance()
        {
            var pgInstance = new EmbeddedPostgres(
                new Version("9.4.15"));

            pgInstance.Start();

            pgInstance.Stop();
        }
    }
}
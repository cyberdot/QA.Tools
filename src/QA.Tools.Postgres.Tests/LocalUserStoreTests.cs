using System.Runtime.InteropServices;
using FluentAssertions;
using QA.Tools.Postgres.Config;
using QA.Tools.Postgres.Distribution;
using QA.Tools.Postgres.Store;
using Xunit;

namespace QA.Tools.Postgres.Tests
{
    public class LocalUserStoreTests
    {
        [Fact]
        public void Should_check_for_distribution()
        {
            var localUserStore = new LocalUserStore(new DefaultStoreConfig());
            var distribution = new Distribution.Distribution(new Version("9.4.15"), OSPlatform.Windows, Architecture.X86);

            var result = localUserStore.GetDistributionPackage(distribution);

            result.Should().NotBeNull();
        }
    }
}
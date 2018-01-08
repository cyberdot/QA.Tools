using System.Runtime.InteropServices;
using System.Threading.Tasks;
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
        public async Task Should_check_for_distribution()
        {
            var localUserStore = new LocalUserStore(new DefaultStoreConfig());
            var distribution = new Distribution.Distribution(new Version("9.4.15"), OSPlatform.Windows, Architecture.X86);

            var result = await localUserStore.GetDistributionPackage(distribution);

            result.Should().NotBeNull();
        }
    }
}
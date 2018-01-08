using System.Threading.Tasks;
using QA.Tools.Postgres.Distribution;

namespace QA.Tools.Postgres.Store
{
    public interface IArtifactStore
    {
        Task<DistributionPackage> GetDistributionPackage(Distribution.Distribution distribution);
    }
}
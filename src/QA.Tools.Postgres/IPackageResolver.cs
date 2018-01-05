using System.IO;
using System.Threading.Tasks;

namespace QA.Tools.Postgres
{
    public interface IPackageResolver
    {
        Task Setup(Distribution distribution, DirectoryInfo distroPath);
    }
}
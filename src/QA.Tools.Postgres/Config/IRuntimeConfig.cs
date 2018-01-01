using QA.Tools.Postgres.Store;

namespace QA.Tools.Postgres.Config
{
    public interface IRuntimeConfig
    {
        IArtifactStore ArtifactStore { get; }
        bool IsDaemonProcess { get; }
    }
}
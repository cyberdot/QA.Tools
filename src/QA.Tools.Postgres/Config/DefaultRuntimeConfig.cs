using QA.Tools.Postgres.Store;

namespace QA.Tools.Postgres.Config
{
    public class DefaultRuntimeConfig : IRuntimeConfig
    {
        public IArtifactStore ArtifactStore { get; }
        public bool IsDaemonProcess { get; }
    }
}
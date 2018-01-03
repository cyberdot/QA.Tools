using QA.Tools.Postgres.Store;

namespace QA.Tools.Postgres.Config
{
    public class DefaultRuntimeConfig : IRuntimeConfig
    {
        public DefaultRuntimeConfig()
        {
            ArtifactStore = new LocalUserStore(new DefaultArtifactConfig());
            IsDaemonProcess = false;
        }

        public IArtifactStore ArtifactStore { get; }
        public bool IsDaemonProcess { get; }
    }
}
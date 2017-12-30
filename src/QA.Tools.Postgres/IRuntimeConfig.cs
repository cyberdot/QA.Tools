namespace QA.Tools.Postgres
{
    public interface IRuntimeConfig
    {
        IArtifactStore ArtifactStore { get; }
        bool IsDaemonProcess { get; }
    }
}
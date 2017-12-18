namespace QA.Tools.Postgres
{
    public interface IRuntimeConfig
    {
        ProcessOutput ProcessOutput { get;  }

        CommandLinePostProcessor CommandLinePostProcessor { get; }

        IArtifactStore ArtifactStore { get; }

        bool IsDaemonProcess { get; }

        ImmutableRuntimeConfig.Builder Builder { get; }
    }
}
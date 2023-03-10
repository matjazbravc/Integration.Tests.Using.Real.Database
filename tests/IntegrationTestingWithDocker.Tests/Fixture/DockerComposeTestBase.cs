using Ductus.FluentDocker.Services;

namespace IntegrationTestingWithDocker.Tests.Fixture;

public abstract class DockerComposeTestBase : IDisposable
{
    /// <summary>
    /// Compose service is a tool for defining and running multi-container Docker applications.
    /// https://docs.docker.com/compose/
    /// </summary>
    protected ICompositeService? CompositeService;
    protected IHostService? DockerHost;

    protected abstract ICompositeService? Build();

    protected DockerComposeTestBase()
    {
        EnsureDockerHost();

        OnContainerBeforeInitialized();

        CompositeService = Build();
        try
        {
            CompositeService?.Start();
        }
        catch
        {
            CompositeService?.Dispose();
            throw;
        }

        OnContainerInitialized();
    }

    public void Dispose()
    {
        OnContainerTearDown();

        var service = CompositeService;
        CompositeService = null;
        try
        {
            service?.Dispose();
        }
        catch
        {
            // Ignore
        }
    }

    /// <summary>
    ///   Invoked just before the container is initialized.
    /// </summary>
    protected virtual void OnContainerBeforeInitialized()
    {
    }

    /// <summary>
    ///   Invoked just before the container is teared down.
    /// </summary>
    protected virtual void OnContainerTearDown()
    {
    }

    /// <summary>
    ///   Invoked after a container has been created and started.
    /// </summary>
    protected virtual void OnContainerInitialized()
    {
    }

    /// <summary>
    /// Ensure that Docker host is up and running
    /// </summary>
    private void EnsureDockerHost()
    {
        if (DockerHost?.State == ServiceRunningState.Running)
        {
            return;
        }

        IList<IHostService>? hosts = new Hosts().Discover();
        DockerHost = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");
        if (DockerHost != null)
        {
            if (DockerHost.State != ServiceRunningState.Running)
            {
                DockerHost.Start();
            }
            return;
        }

        if (hosts.Any())
        {
            DockerHost = hosts.First();
        }
    }
}
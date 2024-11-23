using Common.Message;
using MassTransit;

namespace HrDirector;

public class HrDirectorWorker(
    IPublishEndpoint publishEndpoint,
    IHostApplicationLifetime appLifetime
) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => RunAsync(cancellationToken), cancellationToken);
        return Task.CompletedTask;
    }

    private async void RunAsync(CancellationToken cancellationToken)
    {
        await publishEndpoint.Publish(new StartHackathonMessage { HackathonId = 1 }, cancellationToken);
        // appLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
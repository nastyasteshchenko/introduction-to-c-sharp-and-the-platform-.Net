using Microsoft.Extensions.Hosting;
using Nsu.Hackathon.Problem.Manager;

namespace Nsu.Hackathon.Problem;

public class HackathonWorker(HackathonEventsManager hackathonEventsManager) : IHostedService
{
    private const int Times = 1000;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        hackathonEventsManager.StartEventCertainTimes(Times);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
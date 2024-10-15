using Microsoft.Extensions.Hosting;

namespace Nsu.Hackathon.Problem;

public class HackathonWorker(HackathonEvent hackathonEvent) : IHostedService
{
    private const int Times = 1000;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        for (var i = 0; i < Times; i++)
        {
            Console.WriteLine($"Hackathon № {i + 1} started.");
            hackathonEvent.Start();
        }
        hackathonEvent.PrintSummarizedCompletedHackathonsStatistics();
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
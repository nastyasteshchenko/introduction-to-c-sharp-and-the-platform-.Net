using Microsoft.Extensions.Hosting;

namespace Nsu.Hackathon.Problem;

public class HackathonWorker(HackathonEvent hackathonEvent, IHostApplicationLifetime appLifetime) : IHostedService
{
    private const int Times = 1000;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(RunAsync, cancellationToken);
        return Task.CompletedTask;
    }

    private void RunAsync()
    {
        for (var i = 0; i < Times; i++)
        {
            Console.WriteLine($"Hackathon № {i + 1} started.");
            hackathonEvent.Start();
        }

        hackathonEvent.PrintSummarizedCompletedHackathonsStatistics();
        
        appLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
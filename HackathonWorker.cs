using Microsoft.Extensions.Hosting;
using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.Worker;

namespace Nsu.Hackathon.Problem;

public class HackathonWorker(
    EmployeeRepository employeeRepository,
    HackathonEvent hackathonEvent,
    IHostApplicationLifetime appLifetime) : IHostedService
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
            var juniorsWishlists =
                WishlistsGenerator.GenerateWishlists(employeeRepository.Juniors,
                    employeeRepository.TeamLeads);
            var teamLeadsWishlists =
                WishlistsGenerator.GenerateWishlists(employeeRepository.TeamLeads,
                    employeeRepository.Juniors);
            
            Console.WriteLine($"Hackathon № {i + 1} started.");
            hackathonEvent.Start(juniorsWishlists, teamLeadsWishlists);
        }

        hackathonEvent.PrintSummarizedCompletedHackathonsStatistics();

        appLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
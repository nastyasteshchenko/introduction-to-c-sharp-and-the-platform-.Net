using Microsoft.Extensions.Hosting;
using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.Worker;

namespace Nsu.Hackathon.Problem;

public class HackathonWorker(
    EmployeeRepository employeeRepository,
    HackathonEvent hackathonEvent,
    IHostApplicationLifetime appLifetime,
    HackathonContext hackathonContext
) : IHostedService
{
    private const int Times = 2;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(RunAsync, cancellationToken);
        return Task.CompletedTask;
    }

    private void RunAsync()
    {
        var juniors = employeeRepository.Juniors;
        var teamLeads = employeeRepository.TeamLeads;

        hackathonContext.Juniors.AddRange(juniors);
        hackathonContext.TeamLeads.AddRange(teamLeads);

        hackathonContext.SaveChanges();

        for (var i = 0; i < Times; i++)
        {
            var juniorsWishlists = GenerateWishlists(juniors, teamLeads);
            var teamLeadsWishlists = GenerateWishlists(teamLeads, juniors);

            Console.WriteLine($"Hackathon № {i + 1} started.");
            hackathonEvent.Start(teamLeadsWishlists, juniorsWishlists);
        }

        hackathonEvent.PrintSummarizedCompletedHackathonsStatistics();

        hackathonContext.SaveChanges();
        appLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private List<Wishlist> GenerateWishlists(List<EmployeeEntity> employees, List<EmployeeEntity> desiredEmployees)
    {
        var wishlists =
            WishlistsGenerator.GenerateWishlists(employees, desiredEmployees);

        hackathonContext.Wishlists.AddRange(wishlists);
        hackathonContext.SaveChanges();

        return employees.Select(e =>
            new Wishlist(e, e.Wishlists.Select(w => w.DesiredEmployee).ToList())).ToList();
    }
}
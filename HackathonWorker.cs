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
            var juniorsPreferences = GeneratePreferences(juniors, teamLeads);
            var teamLeadsPreferences = GeneratePreferences(teamLeads, juniors);

            Console.WriteLine($"Hackathon № {i + 1} started.");
            hackathonEvent.Start(teamLeadsPreferences, juniorsPreferences);
        }

        hackathonEvent.PrintSummarizedCompletedHackathonsStatistics();

        hackathonContext.SaveChanges();
        appLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private List<Preference> GeneratePreferences(List<Employee> employees, List<Employee> desiredEmployees)
    {
        var preferences =
            PreferencesGenerator.GeneratePreferences(employees, desiredEmployees);

        List<Wishlist> wishlistsEntities = [];

        foreach (var wishlist in preferences)
        {
            var we = wishlist.DesiredEmployees.Select(de => new Wishlist
            {
                Employee = wishlist.Employee,
                DesiredEmployee = de,
                PriorityNumber = wishlist.DesiredEmployees.IndexOf(de)
            });
            wishlistsEntities.AddRange(we);
        }

        hackathonContext.Wishlists.AddRange(wishlistsEntities);
        hackathonContext.SaveChanges();

        return preferences;
    }
}
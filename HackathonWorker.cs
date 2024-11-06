using Microsoft.Extensions.Hosting;
using Nsu.Hackathon.Problem.Hackathon;
using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.Worker;

namespace Nsu.Hackathon.Problem;

public class HackathonWorker(
    EmployeeRepository employeeRepository,
    HackathonEvent hackathonEvent,
    IHostApplicationLifetime appLifetime,
    HackathonRepository hackathonRepository
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
        hackathonRepository.EnsureCreated();

        hackathonRepository.AddEmployeesIfItIsEmpty(employeeRepository);

        var juniors = hackathonRepository.GetJuniors();
        var teamLeads = hackathonRepository.GetTeamLeads();

        for (var i = 0; i < Times; i++)
        {
            var juniorsPreferences =
                PreferencesGenerator.GeneratePreferences(juniors, teamLeads);
            var teamLeadsPreferences =
                PreferencesGenerator.GeneratePreferences(teamLeads, juniors);

            Console.WriteLine($"Hackathon № {i + 1} started.");

            var hackathonId = hackathonEvent.Start(juniors.Concat(teamLeads).ToList(), teamLeadsPreferences,
                juniorsPreferences);

            hackathonEvent.PrintHackathonInfo(hackathonId);
        }

        hackathonEvent.PrintAllHackathonsHarmonicMean();
        appLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
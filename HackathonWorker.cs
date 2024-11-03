using Microsoft.Extensions.Hosting;
using Nsu.Hackathon.Problem.Hackathon;
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

        hackathonContext.BulkInsert(juniors, options =>
        {
            options.InsertIfNotExists = true;
            options.ColumnPrimaryKeyExpression = customer => customer.Id;
        });
        hackathonContext.BulkInsert(teamLeads, options =>
        {
            options.InsertIfNotExists = true;
            options.ColumnPrimaryKeyExpression = customer => customer.Id;
        });

        for (var i = 0; i < Times; i++)
        {
            var juniorsPreferences = PreferencesGenerator.GeneratePreferences(juniors, teamLeads);
            var teamLeadsPreferences = PreferencesGenerator.GeneratePreferences(teamLeads, juniors);

            Console.WriteLine($"Hackathon № {i + 1} started.");

            var hackathon = hackathonEvent.Start(juniors.Concat(teamLeads).ToList(), teamLeadsPreferences,
                juniorsPreferences);

            hackathonContext.BulkInsert([hackathon]);
        }

        hackathonEvent.PrintSummarizedCompletedHackathonsStatistics();
        appLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
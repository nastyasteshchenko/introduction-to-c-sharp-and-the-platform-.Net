using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nsu.Hackathon.Problem.TeamBuilding;

namespace Nsu.Hackathon.Problem;

using Worker;

public static class MainProgram
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddHostedService<HackathonWorker>();
                services.AddTransient<HackathonEvent>();
                services.AddTransient<ITeamBuildingStrategy, TeamBuildingStrategy>();
                services.AddTransient<HrManager>();
                services.AddTransient<HrDirector>();
                services.AddTransient<EmployeeRepository>();
            })
            .Build();

        host.Run();
    }
}
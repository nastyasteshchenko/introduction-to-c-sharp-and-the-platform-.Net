using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nsu.Hackathon.Problem.Hackathon;
using Nsu.Hackathon.Problem.TeamBuilding;

namespace Nsu.Hackathon.Problem;

using Worker;

public static class MainProgram
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
                logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            })
            .ConfigureServices((_, services) =>
            {
                services.AddHostedService<HackathonWorker>();
                services.AddTransient<HackathonEvent>();
                services.AddTransient<ITeamBuildingStrategy, TeamBuildingStrategy>();
                services.AddTransient<HrManager>();
                services.AddTransient<HrDirector>();
                services.AddTransient<EmployeeRepository>();
                services.AddTransient<HackathonRepository>();
                services.AddDbContext<HackathonContext>(options =>
                    options.UseSqlServer("Server=localhost;Database=hackathon-problem;" +
                                         "User Id=sa;Password=strongPassword123;TrustServerCertificate=True"));
            })
            .Build();

        host.Run();
    }
}
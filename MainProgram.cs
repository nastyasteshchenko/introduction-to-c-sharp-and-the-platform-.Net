﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nsu.Hackathon.Problem.TeamBuilding;

namespace Nsu.Hackathon.Problem;

using Worker;
using Manager;

public static class MainProgram
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<HackathonWorker>();
                services.AddTransient<HackathonEvent>();
                services.AddTransient<TeamBuildingStrategy>();
                services.AddTransient<HrManager>();
                services.AddTransient<HrDirector>();
                services.AddTransient<HackathonEventsManager>();
                services.AddTransient<EmployeeRepository>();
            })
            .Build();

        host.Run();
    }
}
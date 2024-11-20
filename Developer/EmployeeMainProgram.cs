using Nsu.Hackathon.Problem.Common.Mapper;

namespace Nsu.Hackathon.Problem.Developer;

public static class EmployeeMainProgram
{
    public static void Main(string[] args)
    {
        var type = args[0];
        var id = int.Parse(args[1]);

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton(new EmployeeWorkerOptions(type, id));
                services.AddHostedService<EmployeeWorker>();
                services.AddTransient<EmployeeRepository>();
                services.AddTransient<EmployeeService>();
                services.AddTransient<PreferenceMapper>();
                services.AddTransient<EmployeeMapper>();
            })
            .Build();

        host.Run();
    }
}
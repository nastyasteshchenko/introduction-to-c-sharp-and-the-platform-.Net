using Common.Mapper;

namespace Developer;

public static class EmployeeMainProgram
{
    public static void Main(string[] args)
    {
        var role = Environment.GetEnvironmentVariable("ROLE");
        var id = long.Parse(Environment.GetEnvironmentVariable("ID")!);
        
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton(new EmployeeWorkerOptions(role!, id));
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
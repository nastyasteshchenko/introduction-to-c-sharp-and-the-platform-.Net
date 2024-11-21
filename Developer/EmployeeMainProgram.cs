using Common.Mapper;

namespace Developer;

public static class EmployeeMainProgram
{
    public static void Main(string[] args)
    {
        var type = Environment.GetEnvironmentVariable("TYPE");
        var id = long.Parse(Environment.GetEnvironmentVariable("ID")!);
        
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton(new EmployeeWorkerOptions(type!, id));
                services.AddHostedService<EmployeeWorker>();
                services.AddSingleton<EmployeeRepository>();
                services.AddSingleton<EmployeeService>();
                services.AddSingleton<PreferenceMapper>();
                services.AddSingleton<EmployeeMapper>();
            })
            .Build();

        host.Run();
    }
}
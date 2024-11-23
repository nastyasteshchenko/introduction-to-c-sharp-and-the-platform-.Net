using Common.Mapper;
using MassTransit;

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
                services.AddSingleton<EmployeeRepository>();
                services.AddSingleton<EmployeeService>();
                services.AddSingleton<PreferenceMapper>();
                services.AddSingleton<EmployeeMapper>();
                services.AddMassTransit(x =>
                {
                    x.AddConsumer<StartHackathonMessageConsumer>();
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host("rabbitmq", "/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });
                        cfg.ReceiveEndpoint($"start-hackathon-{Guid.NewGuid()}",
                            e => { e.Consumer<StartHackathonMessageConsumer>(context); });
                    });
                });
            })
            .Build();

        host.Run();
    }
}
using Common.Mapper;
using MassTransit;

namespace Developer;

public static class EmployeeMainProgram
{
    public static void Main(string[] args)
    {
        var type = Environment.GetEnvironmentVariable("TYPE");
        var id = long.Parse(Environment.GetEnvironmentVariable("ID")!);
        var queueName = Environment.GetEnvironmentVariable("QUEUE_NAME")!;
        var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST")!;
        var rabbitMqUsername = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME")!;
        var rabbitMqPassword = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")!;

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton(new Options(type!, id));
                services.AddSingleton<EmployeeRepository>();
                services.AddSingleton<EmployeeService>();
                services.AddSingleton<PreferenceMapper>();
                services.AddSingleton<EmployeeMapper>();
                services.AddMassTransit(x =>
                {
                    x.AddConsumer<HackathonStartedConsumer>();
                    x.AddConsumer<HackathonsStoppedConsumer>();
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(rabbitMqHost, "/", h =>
                        {
                            h.Username(rabbitMqUsername);
                            h.Password(rabbitMqPassword);
                        });
                        cfg.ReceiveEndpoint(queueName,
                            e => { e.Consumer<HackathonStartedConsumer>(context); });
                        cfg.ReceiveEndpoint("stop-queue-" + type + "-" + id,
                            e => { e.Consumer<HackathonsStoppedConsumer>(context); });
                    });
                });
            })
            .Build();

        host.Run();
    }
}
using Common.Mapper;
using HrDirector.DataBase;
using HrDirector.DataBase.Mapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace HrDirector;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST")!;
        var rabbitMqUsername = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME")!;
        var rabbitMqPassword = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")!;
        var hackathonEventTimes = int.Parse(Environment.GetEnvironmentVariable("HACKATHON_EVENT_TIMES")!);

        services.AddSingleton<HackathonInfoPrinter>();
        services.AddSingleton(new Options(hackathonEventTimes));
        services.AddHostedService<HrDirectorWorker>();
        services.AddSingleton<HrDirectorService>();
        services.AddSingleton<TeamMapper>();
        services.AddSingleton<PreferenceMapper>();
        services.AddSingleton<EmployeeMapper>();
        services.AddSingleton<EmployeeEntityMapper>();
        services.AddSingleton<TeamEntityMapper>();
        services.AddSingleton<HackathonEventManager>();
        services.AddSingleton<HackathonRepository>();
        services.AddDbContext<HackathonContext>(options =>
            options.UseNpgsql(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure()));
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqHost, "/", h =>
                {
                    h.Username(rabbitMqUsername);
                    h.Password(rabbitMqPassword);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
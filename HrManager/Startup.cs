using Common.Mapper;
using HrManager.TeamBuilding;
using MassTransit;

namespace HrManager;

internal class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var expectedPreferencesAmount = int.Parse(Environment.GetEnvironmentVariable("PREFS_AMOUNT")!);
        var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST")!;
        var rabbitMqUsername = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME")!;
        var rabbitMqPassword = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")!;

        services.AddMassTransit(x =>
        {
            x.AddConsumer<PreferencesMessageConsumer>();
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
        services.AddSingleton<HrManagerService>();
        services.AddSingleton<TeamMapper>();
        services.AddSingleton<PreferenceMapper>();
        services.AddSingleton<EmployeeMapper>();
        services.AddSingleton(new Options(expectedPreferencesAmount));
        services.AddSingleton<ITeamBuildingStrategy, TeamBuildingStrategy>();
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
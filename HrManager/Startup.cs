using Common.Mapper;
using HrManager.TeamBuilding;
using MassTransit;

namespace HrManager;

internal class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var expectedPreferencesAmount = int.Parse(Environment.GetEnvironmentVariable("PREFS_AMOUNT")!);
        services.AddMassTransit(x =>
        {
            x.AddConsumer<PreferencesMessageConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint("start-hackathon-service",
                    e => { e.Consumer<PreferencesMessageConsumer>(context); });
            });
        });
        services.AddSingleton<HrManagerService>();
        services.AddSingleton<TeamMapper>();
        services.AddSingleton<PreferenceMapper>();
        services.AddSingleton<EmployeeMapper>();
        services.AddSingleton(new HrManagerControllerOptions(expectedPreferencesAmount));
        services.AddSingleton<ITeamBuildingStrategy, TeamBuildingStrategy>();
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
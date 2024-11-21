using Common.Mapper;
using HrManager.TeamBuilding;
using Microsoft.Extensions.DependencyInjection;

namespace HrManager;

internal class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<HrManagerService>();
        services.AddTransient<TeamMapper>();
        services.AddTransient<PreferenceMapper>();
        services.AddTransient<EmployeeMapper>();
        services.AddSingleton(new HrManagerControllerOptions(10));
        services.AddTransient<ITeamBuildingStrategy, TeamBuildingStrategy>();
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
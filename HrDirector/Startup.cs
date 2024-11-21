using Common.Mapper;
using HrDirector.DataBase;
using HrDirector.DataBase.Mapper;
using Microsoft.EntityFrameworkCore;

namespace HrDirector;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        services.AddSingleton<HrDirectorService>();
        services.AddSingleton<TeamMapper>();
        services.AddSingleton<PreferenceMapper>();
        services.AddSingleton<EmployeeMapper>();
        services.AddSingleton<EmployeeEntityMapper>();
        services.AddSingleton<TeamEntityMapper>();
        services.AddSingleton<HackathonRepository>();
        services.AddDbContext<HackathonContext>(options =>
            options.UseNpgsql(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure()));
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
using Common.Mapper;
using HrDirector.DataBase;
using HrDirector.DataBase.Mapper;
using Microsoft.EntityFrameworkCore;

namespace HrDirector;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<HrDirectorService>();
        services.AddSingleton<TeamMapper>();
        services.AddSingleton<PreferenceMapper>();
        services.AddSingleton<EmployeeMapper>();
        services.AddSingleton<EmployeeEntityMapper>();
        services.AddSingleton<TeamEntityMapper>();
        services.AddSingleton<HackathonRepository>();
        services.AddDbContext<HackathonContext>(options =>
            options.UseNpgsql("Host=db;Port=5432;Database=hackathon-problem;Username=postgres;Password=postgres",
                sqlOptions => sqlOptions.EnableRetryOnFailure()));
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
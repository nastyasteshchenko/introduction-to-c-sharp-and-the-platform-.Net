using Microsoft.EntityFrameworkCore;
using Nsu.Hackathon.Problem.Common.Mapper;
using Nsu.Hackathon.Problem.HrDirector.DataBase;

namespace Nsu.Hackathon.Problem.HrDirector;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<HrDirectorService>();
        services.AddTransient<TeamMapper>();
        services.AddTransient<PreferenceMapper>();
        services.AddTransient<EmployeeMapper>();
        services.AddTransient<HackathonRepository>();
        services.AddDbContext<HackathonContext>(options =>
            options.UseSqlServer("Server=localhost;Database=hackathon-problem;" +
                                 "User Id=sa;Password=strongPassword123;TrustServerCertificate=True"));
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
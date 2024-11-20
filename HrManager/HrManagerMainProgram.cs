namespace Nsu.Hackathon.Problem.HrManager;

public static class HrManagerMainProgram
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseUrls("http://localhost:5001");
            }).Build();
        
        host.Run();
    }
}
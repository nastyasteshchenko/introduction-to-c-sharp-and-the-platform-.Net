namespace HrDirector;

public static class HrDirectorMainProgram
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseUrls("http://localhost:5002");
            }).Build();
        
        host.Run();
    }
}
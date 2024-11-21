using System.Text;
using Common.Mapper;
using Newtonsoft.Json;

namespace Developer;

public class EmployeeWorker(
    EmployeeService employeeService,
    EmployeeWorkerOptions options,
    PreferenceMapper preferenceMapper,
    IHostApplicationLifetime appLifetime
) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(RunAsync, cancellationToken);
        return Task.CompletedTask;
    }

    private async void RunAsync()
    {
        var preference = employeeService.GetPreference(options.Type, options.Id);
        var preferenceDto = preferenceMapper.PreferenceToPreferenceDto(preference);
        var requestBodyJson = JsonConvert.SerializeObject(preferenceDto);
        Console.WriteLine(requestBodyJson);
        using var client = new HttpClient();
        await client.PostAsync("http://localhost:5001/api/hr-manager/preferences",
            new StringContent(requestBodyJson, Encoding.UTF8, "application/json"));
        appLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
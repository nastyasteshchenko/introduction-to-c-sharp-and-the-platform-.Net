namespace HrDirector;

public class HrDirectorWorker(
    HackathonEventManager hackathonEventManager
) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(hackathonEventManager.StartNewHackathon, cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
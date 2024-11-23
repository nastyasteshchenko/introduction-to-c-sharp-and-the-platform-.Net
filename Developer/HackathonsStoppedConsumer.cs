using Common.Message;
using MassTransit;

namespace Developer;

public class HackathonsStoppedConsumer(IHostApplicationLifetime lifetime)
    : IConsumer<HackathonsStopped>
{
    public Task Consume(ConsumeContext<HackathonsStopped> context)
    {
        lifetime.StopApplication();
        return Task.CompletedTask;
    }
}
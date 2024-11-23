using Common.Message;
using MassTransit;

namespace HrManager;

public class SendPreferencesConsumer(HrManagerService hrManagerService) : IConsumer<SendPreferences>
{
    public Task Consume(ConsumeContext<SendPreferences> context)
    {
        hrManagerService.AddPreference(context.Message);
        return Task.CompletedTask;
    }
}
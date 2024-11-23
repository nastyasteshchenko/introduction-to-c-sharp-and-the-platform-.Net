using Common.Message;
using MassTransit;

namespace HrManager;

public class PreferencesMessageConsumer(HrManagerService hrManagerService) : IConsumer<PreferencesMessage>
{
    public Task Consume(ConsumeContext<PreferencesMessage> context)
    {
        hrManagerService.AddPreference(context.Message);
        return Task.CompletedTask;
    }
}
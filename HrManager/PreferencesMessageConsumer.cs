using MassTransit;

namespace HrManager;

public class PreferencesMessageConsumer : IConsumer<PreferencesMessageConsumer>
{
    public Task Consume(ConsumeContext<PreferencesMessageConsumer> context)
    {
        // employeeService.HandleStartHackathonMessage(context.Message);
        return Task.CompletedTask;
    }
}
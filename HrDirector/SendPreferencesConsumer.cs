using Common.Message;
using MassTransit;

namespace HrDirector;

public class SendPreferencesConsumer(HrDirectorService hrDirectorService) : IConsumer<SendPreferences>
{
    public Task Consume(ConsumeContext<SendPreferences> context)
    {
        hrDirectorService.HandleSendPreferencesMessage(context.Message.Preference);
        return Task.CompletedTask;
    }
}
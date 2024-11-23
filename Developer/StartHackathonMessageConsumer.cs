using Common.Message;
using MassTransit;

namespace Developer;

public class StartHackathonMessageConsumer(EmployeeService employeeService) : IConsumer<StartHackathonMessage>
{
    public Task Consume(ConsumeContext<StartHackathonMessage> context)
    {
        employeeService.HandleStartHackathonMessage(context.Message);
        return Task.CompletedTask;
    }
}
using Common.Message;
using MassTransit;

namespace Developer;

public class HackathonStartedConsumer(EmployeeService employeeService) : IConsumer<HackathonStarted>
{
    public Task Consume(ConsumeContext<HackathonStarted> context)
    {
        employeeService.HandleStartHackathonMessage(context.Message);
        return Task.CompletedTask;
    }
}
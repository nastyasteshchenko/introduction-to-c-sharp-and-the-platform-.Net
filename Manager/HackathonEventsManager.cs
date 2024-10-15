using Nsu.Hackathon.Problem.Worker;

namespace Nsu.Hackathon.Problem.Manager;

public class HackathonEventsManager(EmployeeRepository employeeRepository)
{
    public void StartEventCertainTimes(int numberOfTimesToStart)
    {
        var hrDirector = new HrDirector();
        
        var hackathonEvent = new HackathonEvent(employeeRepository, hrDirector);
        
        for (var i = 0; i < numberOfTimesToStart; i++)
        {
            Console.WriteLine($"Hackathon № {i + 1} started.");
            hackathonEvent.Start();
        }

        hrDirector.SummarizeResults();
        hrDirector.SayTotalHackathonsStatistics();
    }
}
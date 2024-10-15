namespace Nsu.Hackathon.Problem.Manager;

public class HackathonEventsManager(HackathonEvent hackathonEvent)
{
    public void StartEventCertainTimes(int numberOfTimesToStart)
    {
        for (var i = 0; i < numberOfTimesToStart; i++)
        {
            Console.WriteLine($"Hackathon № {i + 1} started.");
            hackathonEvent.Start();
        }

        hackathonEvent.PrintSummarizedCompletedHackathonsStatistics();
    }
}
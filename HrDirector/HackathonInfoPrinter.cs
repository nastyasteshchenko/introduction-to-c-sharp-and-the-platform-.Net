using HrDirector.DataBase;

namespace HrDirector;

public class HackathonInfoPrinter(HackathonRepository hackathonRepository)
{
    private const string LineSeparator = "------------------------------------------";

    public void PrintHackathonInfo(long hackathonId)
    {
        var hackathon = hackathonRepository.GetHackathonById(hackathonId);
        if (hackathon is null)
        {
            Console.WriteLine("No hackathon with id " + hackathonId);
            return;
        }

        Console.WriteLine($"Information about hackathon with id {hackathonId}");
        Console.WriteLine("Participants:");
        foreach (var participant in hackathon.Participants)
        {
            Console.WriteLine(participant.Participant);
        }

        Console.WriteLine(LineSeparator);
        Console.WriteLine("Teams:");
        foreach (var team in hackathon.Teams)
        {
            Console.WriteLine(team.TeamEntity);
        }

        Console.WriteLine(LineSeparator);
        Console.WriteLine($"Harmonic mean: {hackathon.HarmonicMean:0.000}");

        PrintAllHackathonsHarmonicMean();
    }

    private void PrintAllHackathonsHarmonicMean()
    {
        var hackathons = hackathonRepository.GetAllHackathons();
        var harmonicMeans = hackathons.Select(h => h.HarmonicMean).ToList();
        var harmonicMeansAverage = harmonicMeans.Average();
        Console.WriteLine(LineSeparator);
        Console.WriteLine($"Total harmonic mean average: {harmonicMeansAverage:0.000}");
        Console.WriteLine(LineSeparator);
    }
}
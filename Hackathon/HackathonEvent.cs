namespace Nsu.Hackathon.Problem.Hackathon;

using Preferences;
using Worker;

public class HackathonEvent(HrManager hrManager, HrDirector hrDirector, HackathonRepository hackathonRepository)
{
    private const string LineSeparator = "------------------------------------------";

    public long Start(List<Employee> participants, List<Preference> teamLeadsPreferences,
        List<Preference> juniorsPreferences)
    {
        var hackathon = new HackathonEntity();

        hackathon.AddParticipants(participants);

        hackathon.AddWishlists(teamLeadsPreferences);
        hackathon.AddWishlists(juniorsPreferences);

        var teams =
            hrManager.BuildTeams(teamLeadsPreferences, juniorsPreferences);

        hackathon.AddTeams(teams);
        hrDirector.SaveStatistics(teams, teamLeadsPreferences, juniorsPreferences);

        hackathon.HarmonicMean = hrDirector.CurrentHackathonHarmonicMean;

        return hackathonRepository.SaveHackathon(hackathon);
    }

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
            Console.WriteLine(team.Team);
        }

        Console.WriteLine(LineSeparator);

        Console.WriteLine($"Harmonic mean: {hackathon.HarmonicMean:0.000}");
    }
    
    public void PrintAllHackathonsHarmonicMean()
    {
        var hackathons = hackathonRepository.GetAllHackathons();
        var harmonicMeans = hackathons.Select(h => h.HarmonicMean).ToList();
        var harmonicMeansAverage = hrDirector.CountTotalHarmonicMeanAverage(harmonicMeans);
        Console.WriteLine(LineSeparator);
        Console.WriteLine($"Total harmonic mean average: {harmonicMeansAverage:0.000}");
    }
}
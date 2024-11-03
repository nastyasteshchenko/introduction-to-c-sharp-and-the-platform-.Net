namespace Nsu.Hackathon.Problem.Hackathon;

using Preferences;
using Worker;

public class HackathonEvent(HrManager hrManager, HrDirector hrDirector)
{
    public HackathonEntity Start(List<Employee> participants, List<Preference> teamLeadsPreferences,
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
        hrDirector.SayCurrentHackathonStatistics();

        hackathon.HarmonicMean = hrDirector.CurrentHackathonHarmonicMean;

        return hackathon;
    }

    public void PrintSummarizedCompletedHackathonsStatistics()
    {
        hrDirector.SummarizeResults();
        hrDirector.SayTotalHackathonsStatistics();
    }
}
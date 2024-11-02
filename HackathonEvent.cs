namespace Nsu.Hackathon.Problem;

using Preferences;
using Worker;

public class HackathonEvent(HrManager hrManager, HrDirector hrDirector)
{
    public void Start(List<Preference> teamLeadsPreferences, List<Preference> juniorsPreferences)
    {
        var teams =
            hrManager.BuildTeams(teamLeadsPreferences, juniorsPreferences);

        hrDirector.SaveStatistics(teams, teamLeadsPreferences, juniorsPreferences);
        hrDirector.SayCurrentHackathonStatistics();
    }

    public void PrintSummarizedCompletedHackathonsStatistics()
    {
        hrDirector.SummarizeResults();
        hrDirector.SayTotalHackathonsStatistics();
    }
}
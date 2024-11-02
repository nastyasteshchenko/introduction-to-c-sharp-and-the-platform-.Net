using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;

namespace Nsu.Hackathon.Problem.Worker;

public class HrManager(ITeamBuildingStrategy teamBuildingStrategy)
{
    public List<Team> BuildTeams(List<Preference> teamLeadsPreferences, List<Preference> juniorsPreferences)
    {
        return teamBuildingStrategy.BuildTeams(teamLeadsPreferences, juniorsPreferences);
    }
}
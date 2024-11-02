using Nsu.Hackathon.Problem.Preferences;

namespace Nsu.Hackathon.Problem.TeamBuilding;

public interface ITeamBuildingStrategy
{
    public List<Team> BuildTeams(List<Preference> teamLeadsPreferences, List<Preference> juniorsPreferences);
}
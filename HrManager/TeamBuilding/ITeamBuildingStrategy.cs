using Nsu.Hackathon.Problem.Common.Model;

namespace Nsu.Hackathon.Problem.HrManager.TeamBuilding;

public interface ITeamBuildingStrategy
{
    public List<Team> BuildTeams(List<Preference> teamLeadsPreferences, List<Preference> juniorsPreferences);
}
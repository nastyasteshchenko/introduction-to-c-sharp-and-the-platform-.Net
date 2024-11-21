using Common.Model;

namespace HrManager.TeamBuilding;

public interface ITeamBuildingStrategy
{
    public List<Team> BuildTeams(List<Preference> teamLeadsPreferences, List<Preference> juniorsPreferences);
}
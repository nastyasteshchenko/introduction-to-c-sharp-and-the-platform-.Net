using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;

namespace Nsu.Hackathon.Problem.Worker;

public class HrManager(TeamBuildingStrategy teamBuildingStrategy)
{
    public List<Team> BuildTeams(List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists)
    {
        return teamBuildingStrategy.BuildTeams(teamLeadsWishlists, juniorsWishlists);
    }
}
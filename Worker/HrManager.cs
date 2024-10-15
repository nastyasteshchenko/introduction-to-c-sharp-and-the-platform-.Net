using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;

namespace Nsu.Hackathon.Problem.Worker;

public static class HrManager
{
    public static List<Team> BuildTeams(List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists)
    {
        return TeamBuildingStrategy.BuildTeams(teamLeadsWishlists, juniorsWishlists);
    }
}
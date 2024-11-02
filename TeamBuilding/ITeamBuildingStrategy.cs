using Nsu.Hackathon.Problem.Preferences;

namespace Nsu.Hackathon.Problem.TeamBuilding;

public interface ITeamBuildingStrategy
{
    public List<TeamEntity> BuildTeams(List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists);
}
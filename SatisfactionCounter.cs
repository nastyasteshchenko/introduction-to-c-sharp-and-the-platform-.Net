namespace Nsu.Hackathon.Problem;

using Preferences;
using TeamBuilding;

public static class SatisfactionCounter
{
    public static List<int> CountSatisfaction(List<Team> teams,
        List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists)
    {
        var juniorsDesiredEmployees =
            juniorsWishlists.ToDictionary(w => w.Employee, w => w.DesiredEmployees);
        var teamLeadsDesiredEmployees =
            teamLeadsWishlists.ToDictionary(w => w.Employee, w => w.DesiredEmployees);

        var satisfaction = new List<int>();
        foreach (var team in teams)
        {
            var teamLeadSatisfactionIndex = juniorsWishlists.Count -
                                            teamLeadsDesiredEmployees[team.TeamLead].IndexOf(team.Junior);
            var juniorSatisfactionIndex = teamLeadsWishlists.Count -
                                          juniorsDesiredEmployees[team.Junior].IndexOf(team.TeamLead);
            satisfaction.Add(teamLeadSatisfactionIndex);
            satisfaction.Add(juniorSatisfactionIndex);
        }

        return satisfaction;
    }
}
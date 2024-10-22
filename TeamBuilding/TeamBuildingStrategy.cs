namespace Nsu.Hackathon.Problem.TeamBuilding;

using Preferences;
using Worker;

public class TeamBuildingStrategy : ITeamBuildingStrategy
{
    private const Employee? NoPair = null;

    public List<Team> BuildTeams(List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists)
    {
        var juniors = juniorsWishlists.Select(x => x.Employee).ToList();
        var teamLeads = teamLeadsWishlists.Select(x => x.Employee).ToList();

        var teamLeadsPartners = teamLeads.ToDictionary(junior => junior, _ => NoPair);

        var juniorsDesiredEmployees =
            juniorsWishlists.ToDictionary(w => w.Employee, w => w.DesiredEmployees);
        var teamLeadsDesiredEmployees =
            teamLeadsWishlists.ToDictionary(w => w.Employee, w => w.DesiredEmployees);

        var freeJuniors = new Queue<Employee>();
        foreach (var employee in juniors)
        {
            freeJuniors.Enqueue(employee);
        }

        while (freeJuniors.Count > 0)
        {
            var junior = freeJuniors.Dequeue();
            var juniorPreferences = juniorsDesiredEmployees[junior];

            foreach (var preferTeamLead in juniorPreferences)
            {
                var currentTeamLeadPartner = teamLeadsPartners[preferTeamLead];
                if (currentTeamLeadPartner == NoPair)
                {
                    teamLeadsPartners[preferTeamLead] = junior;
                    break;
                }

                var teamLeadPreferences = teamLeadsDesiredEmployees[preferTeamLead];
                if (TeamLeadPrefersJ1OverJ(teamLeadPreferences, junior, currentTeamLeadPartner))
                {
                    continue;
                }

                teamLeadsPartners[preferTeamLead] = junior;
                freeJuniors.Enqueue(currentTeamLeadPartner);
                break;
            }
        }

        var teams = teamLeadsPartners
            .Select(entry => new Team(entry.Key, entry.Value!))
            .ToList();

        foreach (var team in teams)
        {
            Console.WriteLine(team);
        }

        return teams;
    }

    private static bool TeamLeadPrefersJ1OverJ(List<Employee> teamLeadPreferences, Employee junior, Employee junior1)
    {
        foreach (var preferJunior in teamLeadPreferences)
        {
            if (preferJunior.Equals(junior1))
            {
                return true;
            }

            if (preferJunior.Equals(junior))
            {
                return false;
            }
        }

        return false;
    }
}
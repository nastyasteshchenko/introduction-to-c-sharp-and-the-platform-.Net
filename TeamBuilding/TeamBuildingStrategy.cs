namespace Nsu.Hackathon.Problem.TeamBuilding;

using Preferences;
using Worker;

public static class TeamBuildingStrategy
{
    private const Employee? NoPair = null;

    public static List<Team> BuildTeams(List<Employee> teamLeads, List<Employee> juniors,
        List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists)
    {
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

            var isFreeCurrentJunior = true;
            foreach (var preferTeamLead in juniorPreferences)
            {
                if (!isFreeCurrentJunior)
                {
                    continue;
                }

                var currentTeamLeadPartner = teamLeadsPartners[preferTeamLead];
                if (currentTeamLeadPartner == NoPair)
                {
                    teamLeadsPartners[preferTeamLead] = junior;
                    isFreeCurrentJunior = false;
                }
                else
                {
                    var teamLeadPreferences = teamLeadsDesiredEmployees[preferTeamLead];
                    if (TeamLeadPrefersJ1OverJ(teamLeadPreferences, junior, currentTeamLeadPartner))
                    {
                        continue;
                    }

                    teamLeadsPartners[preferTeamLead] = junior;
                    isFreeCurrentJunior = false;
                    freeJuniors.Enqueue(currentTeamLeadPartner);
                }
            }
        }

        var teams = teamLeadsPartners
            .Select(entry => new Team(entry.Key, entry.Value!))
            .ToList();

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
using Nsu.Hackathon.Problem.Common.Model;

namespace Nsu.Hackathon.Problem.HrManager.TeamBuilding;

public class TeamBuildingStrategy : ITeamBuildingStrategy
{
    private const Employee? NoPair = null;

    public List<Team> BuildTeams(List<Preference> teamLeadsPreferences, List<Preference> juniorsPreferences)
    {
        var juniors = juniorsPreferences.Select(x => x.Employee).ToList();
        var teamLeads = teamLeadsPreferences.Select(x => x.Employee).ToList();
        
        var teamLeadsPartners = teamLeads.ToDictionary(junior => junior, _ => NoPair);
        
        var juniorsDesiredEmployees =
            juniorsPreferences.ToDictionary(w => w.Employee, w => w.DesiredEmployees);
        var teamLeadsDesiredEmployees =
            teamLeadsPreferences.ToDictionary(w => w.Employee, w => w.DesiredEmployees);
        
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
            .Select(entry =>
            {
                var team = new Team()
                {
                    TeamLead = entry.Key,
                    Junior = entry.Value!
                };
                return team;
            })
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
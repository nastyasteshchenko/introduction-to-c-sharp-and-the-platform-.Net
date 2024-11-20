using Nsu.Hackathon.Problem.Common.Model;

namespace Nsu.Hackathon.Problem.HrDirector.Calculator;

public static class SatisfactionCalculator
{
    public static List<int> CalculateSatisfaction(List<Team> teams,
        List<Preference> teamLeadsWishlists, List<Preference> juniorsWishlists)
    {
        foreach (var f in juniorsWishlists)
        {
            Console.WriteLine(f.Employee);
        }
        
        foreach (var f in teamLeadsWishlists)
        {
            Console.WriteLine(f.Employee);
        }
        
        foreach (var f in teams)
        {
            Console.WriteLine(f);
        }
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
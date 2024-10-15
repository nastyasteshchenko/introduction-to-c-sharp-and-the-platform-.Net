namespace Nsu.Hackathon.Problem.Manager;

using Preferences;
using Worker;

public class HackathonEvent(EmployeeRepository employeeRepository, HrDirector hrDirector)
{
    private readonly List<Employee> _juniors = employeeRepository.Juniors;
    private readonly List<Employee> _teamLeads = employeeRepository.TeamLeads;

    public void Start()
    {
        var juniorsWishlist =
            WishlistsGenerator.GenerateWishlists(_juniors, _teamLeads);
        var teamLeadsWishlist =
            WishlistsGenerator.GenerateWishlists(_teamLeads, _juniors);
        var teams =
            HrManager.BuildTeams(teamLeadsWishlist, juniorsWishlist);

        hrDirector.CalculateStatistics(teams, teamLeadsWishlist, juniorsWishlist);

        hrDirector.SayCurrentHackathonStatistics();
    }
}
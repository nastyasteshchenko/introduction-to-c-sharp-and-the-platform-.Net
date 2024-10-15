namespace Nsu.Hackathon.Problem;

using Preferences;
using Worker;

public class HackathonEvent(EmployeeRepository employeeRepository, HrManager hrManager, HrDirector hrDirector)
{
    public void Start()
    {
        var juniorsWishlist =
            WishlistsGenerator.GenerateWishlists(employeeRepository.Juniors,
                employeeRepository.TeamLeads);
        var teamLeadsWishlist =
            WishlistsGenerator.GenerateWishlists(employeeRepository.TeamLeads,
                employeeRepository.Juniors);
        var teams =
            hrManager.BuildTeams(teamLeadsWishlist, juniorsWishlist);

        hrDirector.SaveStatistics(teams, teamLeadsWishlist, juniorsWishlist);
        hrDirector.SayCurrentHackathonStatistics();
    }

    public void PrintSummarizedCompletedHackathonsStatistics()
    {
        hrDirector.SummarizeResults();
        hrDirector.SayTotalHackathonsStatistics();
    }
}
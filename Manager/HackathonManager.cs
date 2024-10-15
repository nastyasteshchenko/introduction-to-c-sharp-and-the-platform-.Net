namespace Nsu.Hackathon.Problem.Manager;

using Preferences;
using Worker;

public class HackathonManager(EmployeeRepository employeeRepository)
{
    public void StartHackathonCertainTimes(int numberOfTimesToStart)
    {
        var hrDirector = new HrDirector();
        var juniors = employeeRepository.Juniors;
        var teamLeads = employeeRepository.TeamLeads;

        for (var i = 0; i < numberOfTimesToStart; i++)
        {
            Console.WriteLine($"Hackathon № {i + 1} started.");
            var juniorsWishlist =
                WishlistsGenerator.GenerateWishlists(juniors, teamLeads);
            var teamLeadsWishlist =
                WishlistsGenerator.GenerateWishlists(teamLeads, juniors);
            var teams =
                HrManager.BuildTeams(teamLeadsWishlist, juniorsWishlist);

            hrDirector.CalculateStatistics(teams, teamLeadsWishlist, juniorsWishlist);

            hrDirector.SayCurrentHackathonStatistics();
        }

        hrDirector.SummarizeResults();
        hrDirector.SayTotalHackathonsStatistics();
    }
}
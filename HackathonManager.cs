namespace Nsu.Hackathon.Problem;

public class HackathonManager(EmployeeRepository employeeRepository)
{
    public void StartCertainTimes(int numberOfTimesToStart)
    {
        var statisticsManager = new StatisticsManager();
        var juniors = employeeRepository.Juniors;
        var teamLeads = employeeRepository.TeamLeads;

        for (var i = 0; i < numberOfTimesToStart; i++)
        {
            Console.WriteLine($"Hackathon № {i} started.");
            var juniorsWishlist =
                WishlistsGenerator.GenerateWishlists(juniors, teamLeads);
            var teamLeadsWishlist =
                WishlistsGenerator.GenerateWishlists(teamLeads, juniors);
            var teams = TeamBuildingStrategy.BuildTeams(teamLeads, juniors, teamLeadsWishlist, juniorsWishlist);
            
            statisticsManager.AddStatistics(teams, teamLeadsWishlist, juniorsWishlist);

            statisticsManager.PrintCurrentHarmonicMean();
        }

        statisticsManager.SummarizeResults();
        statisticsManager.PrintResults();
    }
}
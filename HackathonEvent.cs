namespace Nsu.Hackathon.Problem;

using Preferences;
using Worker;

public class HackathonEvent(HrManager hrManager, HrDirector hrDirector)
{
    public void Start(List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists)
    {
        var teams =
            hrManager.BuildTeams(teamLeadsWishlists, juniorsWishlists);

        hrDirector.SaveStatistics(teams, teamLeadsWishlists, juniorsWishlists);
        hrDirector.SayCurrentHackathonStatistics();
    }

    public void PrintSummarizedCompletedHackathonsStatistics()
    {
        hrDirector.SummarizeResults();
        hrDirector.SayTotalHackathonsStatistics();
    }
}
using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;
using Nsu.Hackathon.Problem.Worker;

namespace Test.Worker;

public class HrDirectorTest
{
    [Test]
    public void HarmonicMeanForTeamTest()
    {
        var junior1 = CreateJunior(1, "Юдин Адам");
        var junior2 = CreateJunior(2, "Яшина Яна");
        var junior3 = CreateJunior(3, "Никитина Вероника");
        var junior4 = CreateJunior(4, "Рябинин Александр");

        var teamLead1 = CreateTeamLead(5, "Филиппова Ульяна");
        var teamLead2 = CreateTeamLead(6, "Николаев Григорий");
        var teamLead3 = CreateTeamLead(7, "Андреева Вероника");
        var teamLead4 = CreateTeamLead(8, "Коротков Михаил");
    
        var teamLeadsWishlists = new List<Preference>
        {
            new(teamLead1, [junior2, junior1, junior3, junior4]),
            new(teamLead2, [junior1, junior4, junior3, junior2]),
            new(teamLead3, [junior3, junior2, junior1, junior4]),
            new(teamLead4, [junior3, junior4, junior2, junior4]),
        };
    
        var juniorsWishlists = new List<Preference>
        {
            new(junior1, [teamLead1, teamLead2, teamLead3, teamLead4]),
            new(junior2, [teamLead1, teamLead3, teamLead4, teamLead2]),
            new(junior3, [teamLead4, teamLead3, teamLead1, teamLead2]),
            new(junior4, [teamLead2, teamLead4, teamLead3, teamLead1]),
        };
    
        var teams = new List<Team>
        {
            CreateTeam(teamLead1, junior2),
            CreateTeam(teamLead2, junior1),
            CreateTeam(teamLead3, junior4),
            CreateTeam(teamLead4, junior3)
        };
    
        var director = new HrDirector();
        director.SaveStatistics(teams, teamLeadsWishlists, juniorsWishlists);
    
        Assert.That(Math.Round(director.CurrentHackathonHarmonicMean, 2),
            Is.EqualTo(2.59));
        
        teams =
        [
            CreateTeam(teamLead1, junior1),
            CreateTeam(teamLead2, junior2),
            CreateTeam(teamLead3, junior3),
            CreateTeam(teamLead4, junior4)
        ];
        
        director.SaveStatistics(teams, teamLeadsWishlists, juniorsWishlists);
        
        Assert.That(Math.Round(director.CurrentHackathonHarmonicMean, 2),
            Is.EqualTo(2.09));
    }
    
    private static Team CreateTeam(TeamLead teamLead, Junior junior)
    {
        return new Team
        {
            TeamLead = teamLead,
            Junior = junior
        };
    }
    
    private static Junior CreateJunior(long id, string name)
    {
        return new Junior
        {
            Id = id,
            Name = name
        };
    }
    
    private static TeamLead CreateTeamLead(long id, string name)
    {
        return new TeamLead
        {
            Id = id,
            Name = name
        };
    }
}
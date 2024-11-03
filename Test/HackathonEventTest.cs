using Nsu.Hackathon.Problem.Hackathon;
using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;
using Nsu.Hackathon.Problem.Worker;

namespace Test;

public class HackathonEventTest
{
    [Test]
    public void HolingHackathonEventTest()
    {
        var junior1 = CreateJunior(1, "Юдин Адам");
        var junior2 = CreateJunior(2, "Яшина Яна");
        var junior3 = CreateJunior(3, "Никитина Вероника");
        var junior4 = CreateJunior(4, "Рябинин Александр");

        var teamLead1 = CreateTeamLead(1, "Филиппова Ульяна");
        var teamLead2 = CreateTeamLead(2, "Николаев Григорий");
        var teamLead3 = CreateTeamLead(3, "Андреева Вероника");
        var teamLead4 = CreateTeamLead(4, "Коротков Михаил");

        var teamLeadsWishlists = new List<Preference>
        {
            new(teamLead1, [junior2, junior1, junior3, junior4]),
            new(teamLead2, [junior1, junior4, junior3, junior2]),
            new(teamLead3, [junior3, junior2, junior1, junior4]),
            new(teamLead4, [junior3, junior4, junior2, junior4]),
        };

        var juniorsWishlists = new List<Preference>
        {
            new(junior1, [teamLead2, teamLead1, teamLead3, teamLead4]),
            new(junior2, [teamLead1, teamLead3, teamLead4, teamLead2]),
            new(junior3, [teamLead4, teamLead3, teamLead1, teamLead2]),
            new(junior4, [teamLead2, teamLead4, teamLead3, teamLead1]),
        };

        var hrManager = new HrManager(new TeamBuildingStrategy());
        var hrDirector = new HrDirector();
        var hackathonEvent = new HackathonEvent(hrManager, hrDirector);

        hackathonEvent.Start([], teamLeadsWishlists, juniorsWishlists);

        Assert.That(Math.Round(hrDirector.CurrentHackathonHarmonicMean, 2), Is.EqualTo(2.67));
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
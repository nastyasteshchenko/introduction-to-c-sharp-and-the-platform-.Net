using Common.Model;
using Developer;

namespace Test.Preferences;

public class PreferencesGeneratorTest
{
    [Test]
    public void GeneratingWishlistsTest()
    {
        var junior1 = CreateJunior(1, "Юдин Адам");
        var junior2 = CreateJunior(2, "Яшина Яна");
        var junior3 = CreateJunior(3, "Никитина Вероника");
        var junior4 = CreateJunior(4, "Рябинин Александр");

        var teamLead1 = CreateTeamLead(5, "Филиппова Ульяна");
        var teamLead2 = CreateTeamLead(6, "Николаев Григорий");
        var teamLead3 = CreateTeamLead(7, "Андреева Вероника");
        var teamLead4 = CreateTeamLead(8, "Коротков Михаил");

        var juniors = new List<Employee> { junior1, junior2, junior3, junior4 };
        var teamLeads = new List<Employee> { teamLead1, teamLead2, teamLead3, teamLead4 };

        var juniorsWishlists = PreferencesGenerator.GeneratePreferences(juniors, teamLeads);
        var teamLeadsWishlists =
            PreferencesGenerator.GeneratePreferences(teamLeads, juniors);

        Assert.Multiple(() =>
        {
            Assert.That(juniorsWishlists, Has.Count.EqualTo(juniors.Count));
            Assert.That(teamLeadsWishlists, Has.Count.EqualTo(teamLeads.Count));
        });

        foreach (var desiredEmployees
                 in juniorsWishlists.Select(juniorWishlist => juniorWishlist.DesiredEmployees))
        {
            Assert.That(desiredEmployees, Has.Member(teamLead1));
            Assert.That(desiredEmployees, Has.Member(teamLead2));
            Assert.That(desiredEmployees, Has.Member(teamLead3));
            Assert.That(desiredEmployees, Has.Member(teamLead4));
        }

        foreach (var desiredEmployees
                 in teamLeadsWishlists.Select(teamLeadWishlist => teamLeadWishlist.DesiredEmployees))
        {
            Assert.That(desiredEmployees, Has.Member(junior1));
            Assert.That(desiredEmployees, Has.Member(junior2));
            Assert.That(desiredEmployees, Has.Member(junior3));
            Assert.That(desiredEmployees, Has.Member(junior4));
        }

        var teamLeadsFromTeamLeadsWishlists =
            teamLeadsWishlists.Select(teamLeadWishlist => teamLeadWishlist.Employee)
                .ToList();

        Assert.That(teamLeadsFromTeamLeadsWishlists, Has.Member(teamLead1));
        Assert.That(teamLeadsFromTeamLeadsWishlists, Has.Member(teamLead2));
        Assert.That(teamLeadsFromTeamLeadsWishlists, Has.Member(teamLead3));
        Assert.That(teamLeadsFromTeamLeadsWishlists, Has.Member(teamLead4));

        var juniorsFromJuniorsWishlists =
            juniorsWishlists.Select(juniorWishlist => juniorWishlist.Employee)
                .ToList();

        Assert.That(juniorsFromJuniorsWishlists, Has.Member(junior1));
        Assert.That(juniorsFromJuniorsWishlists, Has.Member(junior2));
        Assert.That(juniorsFromJuniorsWishlists, Has.Member(junior3));
        Assert.That(juniorsFromJuniorsWishlists, Has.Member(junior4));
    }

    private static Junior CreateJunior(long id, string name)
    {
        return new Junior(id, name);
    }

    private static TeamLead CreateTeamLead(long id, string name)
    {
        return new TeamLead(id, name);
    }
}
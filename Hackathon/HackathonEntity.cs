using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;
using Nsu.Hackathon.Problem.Worker;

namespace Nsu.Hackathon.Problem.Hackathon;

public class HackathonEntity
{
    public long Id { get; set; }
    public List<HackathonParticipant> Participants { get; set; } = [];
    public List<HackathonWishlist> Wishlists { get; set; } = [];
    public List<HackathonTeam> Teams { get; set; } = [];
    public double HarmonicMean { get; set; }

    public void AddWishlists(List<Preference> preferences)
    {
        foreach (var wishlist in preferences)
        {
            var we = wishlist.DesiredEmployees.Select(de => new HackathonWishlist
            {
                Wishlist = new Wishlist
                {
                    Employee = wishlist.Employee,
                    DesiredEmployee = de,
                    PriorityNumber = wishlist.DesiredEmployees.IndexOf(de)
                },
                HackathonEntity = this
            });
            Wishlists.AddRange(we);
        }
    }

    public void AddParticipants(List<Employee> employees)
    {
        var participants = employees.Select(e => new HackathonParticipant
        {
            Participant = e,
            HackathonEntity = this
        });
        Participants.AddRange(participants);
    }

    public void AddTeams(List<Team> teams)
    {
        var hackathonTeams = teams.Select(team => new HackathonTeam
        {
            Team = team,
            HackathonEntity = this
        });
        Teams.AddRange(hackathonTeams);
    }
}
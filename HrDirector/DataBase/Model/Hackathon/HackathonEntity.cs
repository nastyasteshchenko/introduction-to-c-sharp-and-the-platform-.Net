using System.ComponentModel.DataAnnotations.Schema;
using Common.Model;
using HrDirector.DataBase.Mapper;
using HrDirector.DataBase.Model.Employee;

namespace HrDirector.DataBase.Model.Hackathon;

[Table(name: "Hackathon")]
public class HackathonEntity
{
    public long Id { get; set; }
    public List<HackathonParticipant> Participants { get; set; } = [];
    public List<HackathonWishlist> Wishlists { get; set; } = [];
    public List<HackathonTeam> Teams { get; set; } = [];
    public double HarmonicMean { get; set; }

    public void AddWishlists(List<Preference> preferences, EmployeeEntityMapper employeeEntityMapper)
    {
        foreach (var wishlist in preferences)
        {
            var we = wishlist.DesiredEmployees.Select(de => new HackathonWishlist
            {
                Wishlist = new Wishlist
                {
                    EmployeeEntity = employeeEntityMapper.EmployeeToEmployeeEntity(wishlist.Employee),
                    DesiredEmployeeEntity = employeeEntityMapper.EmployeeToEmployeeEntity(de),
                    PriorityNumber = wishlist.DesiredEmployees.IndexOf(de)
                },
                HackathonEntity = this
            });
            Wishlists.AddRange(we);
        }
    }

    public void AddParticipants(List<EmployeeEntity> employees)
    {
        var participants = employees.Select(e => new HackathonParticipant
        {
            Participant = e,
            HackathonEntity = this
        });
        Participants.AddRange(participants);
    }

    public void AddTeams(List<TeamEntity> teams)
    {
        var hackathonTeams = teams.Select(team => new HackathonTeam
        {
            TeamEntity = team,
            HackathonEntity = this
        });
        Teams.AddRange(hackathonTeams);
    }
}
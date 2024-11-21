using Microsoft.EntityFrameworkCore;
using Nsu.Hackathon.Problem.HrDirector.DataBase.Model.Employee;
using Nsu.Hackathon.Problem.HrDirector.DataBase.Model.Hackathon;

namespace Nsu.Hackathon.Problem.HrDirector.DataBase;

public class HackathonRepository(HackathonContext hackathonContext)
{
    public HackathonEntity? GetHackathonById(long hackathonId)
    {
        return hackathonContext.Hackathons
            .Include(h => h.Participants)
            .ThenInclude(p => p.Participant)
            .Include(h => h.Wishlists)
            .Include(h => h.Teams)
            .AsSplitQuery()
            .FirstOrDefault(h => h.Id == hackathonId);
    }

    public List<HackathonEntity> GetAllHackathons()
    {
        return hackathonContext.Hackathons
            .ToList();
    }

    public List<EmployeeEntity> GetJuniors()
    {
        return hackathonContext.Juniors.Cast<EmployeeEntity>().ToList();
    }

    public List<EmployeeEntity> GetTeamLeads()
    {
        return hackathonContext.TeamLeads.Cast<EmployeeEntity>().ToList();
    }

    public long SaveHackathon(HackathonEntity hackathon)
    {
        hackathonContext.Hackathons.Add(hackathon);
        hackathonContext.SaveChanges();
        return hackathon.Id;
    }

    public void EnsureCreated()
    {
        hackathonContext.Database.EnsureCreated();
    }
}
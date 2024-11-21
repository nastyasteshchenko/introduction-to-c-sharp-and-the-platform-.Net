using HrDirector.DataBase.Model.Hackathon;
using Microsoft.EntityFrameworkCore;

namespace HrDirector.DataBase;

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
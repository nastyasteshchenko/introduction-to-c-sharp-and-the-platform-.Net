using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;
using Nsu.Hackathon.Problem.Worker;

namespace Nsu.Hackathon.Problem;

public class Hackathon
{
    public long Id { get; set; }
    public List<EmployeeEntity> Juniors { get; set; } = [];
    public List<EmployeeEntity> TeamLeads { get; set; } = [];
    public List<WishlistEntity> JuniorsWishlists { get; set; } = [];
    public List<WishlistEntity> TeamLeadsWishlists { get; set; } = [];
    public List<TeamEntity> Teams { get; set; } = [];
    public double HarmonicMean { get; set; }
}
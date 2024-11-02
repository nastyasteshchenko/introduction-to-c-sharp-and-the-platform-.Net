using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;
using Nsu.Hackathon.Problem.Worker;

namespace Nsu.Hackathon.Problem;

public class Hackathon
{
    public long Id { get; set; }
    public List<Employee> Juniors { get; set; } = [];
    public List<Employee> TeamLeads { get; set; } = [];
    public List<Wishlist> JuniorsWishlists { get; set; } = [];
    public List<Wishlist> TeamLeadsWishlists { get; set; } = [];
    public List<Team> Teams { get; set; } = [];
    public double HarmonicMean { get; set; }
}
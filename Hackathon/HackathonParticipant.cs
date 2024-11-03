using Nsu.Hackathon.Problem.Worker;

namespace Nsu.Hackathon.Problem.Hackathon;

public class HackathonParticipant
{
    public long Id { get; set; }
    public long HackathonId { get; set; }
    public HackathonEntity HackathonEntity { get; set; } = null!;
    public long ParticipantId { get; set; }
    public Employee Participant { get; set; } = null!;
}
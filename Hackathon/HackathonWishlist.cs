using Nsu.Hackathon.Problem.Preferences;

namespace Nsu.Hackathon.Problem.Hackathon;

public class HackathonWishlist
{
    public long Id { get; set; }
    public long HackathonId { get; set; }
    public HackathonEntity HackathonEntity { get; set; } = null!;
    public long WishlistId { get; set; }
    public Wishlist Wishlist { get; set; } = null!;
}
using Microsoft.EntityFrameworkCore;
using Nsu.Hackathon.Problem.Hackathon;
using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;
using Nsu.Hackathon.Problem.Worker;

namespace Nsu.Hackathon.Problem;

public sealed class HackathonContext : DbContext
{
    public DbSet<HackathonEntity> Hackathons { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public HackathonContext()
    {
        Database.EnsureCreated();
    }

    protected override void
        OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionString = "Server=localhost;Database=hackathon-problem;" +
                                        "User Id=sa;Password=strongPassword123;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        BuildHackathonEntityModel(modelBuilder);
        BuildHackathonWishlistModel(modelBuilder);
        BuildHackathonTeamModel(modelBuilder);
        BuildHackathonParticipantModel(modelBuilder);

        BuildWishlistModel(modelBuilder);
        BuildEmployeeModel(modelBuilder);
        BuildTeamModel(modelBuilder);
    }

    private static void BuildTeamModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>()
            .HasOne(e => e.TeamLead)
            .WithMany()
            .HasForeignKey(t => t.TeamLeadId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Team>()
            .HasOne(e => e.Junior)
            .WithMany()
            .HasForeignKey(t => t.JuniorId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void BuildEmployeeModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasMany<Wishlist>(e => e.Wishlists)
            .WithOne(w => w.Employee);
    }

    private static void BuildWishlistModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wishlist>()
            .HasOne(w => w.Employee)
            .WithMany()
            .HasForeignKey(w => w.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Wishlist>()
            .HasOne(w => w.DesiredEmployee)
            .WithMany()
            .HasForeignKey(w => w.DesiredEmployeeId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void BuildHackathonParticipantModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HackathonParticipant>()
            .HasOne(p => p.Participant)
            .WithMany()
            .HasForeignKey(p => p.ParticipantId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<HackathonParticipant>()
            .HasOne(p => p.HackathonEntity)
            .WithMany()
            .HasForeignKey(h => h.HackathonId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void BuildHackathonTeamModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HackathonTeam>()
            .HasOne(t => t.Team)
            .WithMany()
            .HasForeignKey(t => t.TeamId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<HackathonTeam>()
            .HasOne(p => p.HackathonEntity)
            .WithMany()
            .HasForeignKey(h => h.HackathonId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void BuildHackathonWishlistModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HackathonWishlist>()
            .HasOne(w => w.Wishlist)
            .WithMany()
            .HasForeignKey(w => w.WishlistId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<HackathonWishlist>()
            .HasOne(w => w.HackathonEntity)
            .WithMany()
            .HasForeignKey(w => w.HackathonId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void BuildHackathonEntityModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HackathonEntity>()
            .HasMany(h => h.Participants)
            .WithOne(p => p.HackathonEntity)
            .HasForeignKey(p => p.HackathonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HackathonEntity>()
            .HasMany(h => h.Wishlists)
            .WithOne(w => w.HackathonEntity)
            .HasForeignKey(w => w.HackathonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HackathonEntity>()
            .HasMany(h => h.Teams)
            .WithOne(t => t.HackathonEntity)
            .HasForeignKey(t => t.HackathonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
using Microsoft.EntityFrameworkCore;
using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;
using Nsu.Hackathon.Problem.Worker;

namespace Nsu.Hackathon.Problem;

public sealed class HackathonContext : DbContext
{
    // public DbSet<Hackathon> Hackathons { get; set; }
    public DbSet<Employee> Juniors { get; set; }
    public DbSet<Employee> TeamLeads { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    // public DbSet<Team> Teams { get; set; }

    public HackathonContext()
    {
        Database.EnsureCreated();
    }
    protected override void 
        OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = 
            "Server=localhost;Database=hackathon-problem;User Id=sa;Password=strongPassword123;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
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

        modelBuilder.Entity<Employee>()
            .HasMany<Wishlist>(e => e.Wishlists)
            .WithOne(w => w.Employee);
    }
}
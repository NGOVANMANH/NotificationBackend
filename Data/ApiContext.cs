namespace notify.Data;

using Microsoft.EntityFrameworkCore;
using notify.Models;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Notification> Notifications { get; set; } = null!;

    public DbSet<Preference> Preferences { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Notification>()
            .Property(n => n.CreatedAt)
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<User>()
            .HasOne(u => u.Preference)
            .WithOne(p => p.User)
            .HasForeignKey<User>(u => u.PreferenceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

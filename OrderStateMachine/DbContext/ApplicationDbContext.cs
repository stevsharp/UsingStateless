
using Microsoft.EntityFrameworkCore;

using OrderStateMachine.Model;

namespace OrderStateMachine;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Step> Steps { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");

            entity.HasKey(o => o.Id);

            entity.Property(o => o.State)
                .HasConversion(
                    v => v.ToString(),
                    v => (OrderState)Enum.Parse(typeof(OrderState), v))
                .IsRequired();

            entity.Property(o => o.CustomerId)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(o => o.Status)
                .HasMaxLength(20)
                .HasDefaultValue("In Progress");
        });


        modelBuilder.Entity<Step>(entity =>
        {
            entity.ToTable("Steps");

            entity.HasKey(s => s.Id);

            entity.HasOne(s => s.Order)
                .WithMany(o => o.Steps)
                .HasForeignKey(s => s.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(s => s.StepOrder)
                .IsRequired();

            entity.Property(s => s.IsCompleted)
                .IsRequired()
                .HasDefaultValue(false);
        });


        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comments");

            entity.HasKey(c => c.Id);

            entity.HasOne(c => c.Step)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.StepId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(c => c.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}
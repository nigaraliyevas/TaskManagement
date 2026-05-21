using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(t => t.Description)
                .HasMaxLength(2000)
                .HasDefaultValue(string.Empty);

            entity.Property(t => t.Priority)
                .IsRequired();

            entity.Property(t => t.IsCompleted)
                .HasDefaultValue(false);

            // UTC timestamp
            entity.Property(t => t.Deadline)
                .HasColumnType("timestamp with time zone");

            // UTC timestamp
            entity.Property(t => t.CreatedAt)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("NOW()");

            entity.Ignore(t => t.Status);
        });

        base.OnModelCreating(modelBuilder);
    }
}
using Microsoft.EntityFrameworkCore;
using Taskivo_Infrastructure.Models;

namespace Taskivo_Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<TaskEntity> Tasks { get; set; }
    public virtual DbSet<ReminderEntity> Reminders { get; set; }
    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<StatusEntity> Statuses { get; set; }
    public virtual DbSet<PriorityEntity> Priorities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tasks_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CompletedAt).HasColumnName("completed_at");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedByUserId).HasColumnName("created_by_user_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.StatusDetails)
                .WithMany()
                .HasForeignKey(e => e.Status)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.PriorityDetails)
                .WithMany()
                .HasForeignKey(e => e.Priority)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ReminderEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Reminder_pkey");
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("id");
            entity.Property(e => e.UserId)
                .HasColumnName("user_id")
                .IsRequired();
            entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(200)
                .IsRequired();
            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(1000);
            entity.Property(e => e.ReminderAt)
                .HasColumnName("reminder_at")
                .IsRequired();
            entity.Property(e => e.IsSent)
                .HasColumnName("is_sent")
                .HasDefaultValue(false);
            entity.Property(e => e.SentAt)
                .HasColumnName("sent_at");
            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("NOW()");
            entity.Property(e => e.UpdatedAt)
               .HasColumnName("updated_at");
            entity.Property(e => e.DeletedAt)
               .HasColumnName("deleted_at");
             entity.HasIndex(e => new
                {
                    e.UserId,
                    e.ReminderAt
                });
            entity.HasIndex(e => new
                {
                    e.ReminderAt,
                    e.IsSent,
                    e.DeletedAt
                });
            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("username");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasColumnName("password_hash");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasIndex(e => e.Username)
                .IsUnique();
        });

        modelBuilder.Entity<StatusEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("name");
            entity.Property(e => e.ColorHex)
                .HasMaxLength(7)
                .IsRequired()
                .HasColumnName("color_hex");

            entity.HasData(
                new StatusEntity { Id = 1, Name = "New", ColorHex = "#2563EB", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new StatusEntity { Id = 2, Name = "In-Progress", ColorHex = "#F59E0B", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new StatusEntity { Id = 3, Name = "On Hold", ColorHex = "#A78BFA", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new StatusEntity { Id = 4, Name = "Completed", ColorHex = "#22C55E", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new StatusEntity { Id = 5, Name = "Discarded", ColorHex = "#EF4444", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );
        });

        modelBuilder.Entity<PriorityEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("name");
            entity.Property(e => e.ColorHex)
                .HasMaxLength(7)
                .IsRequired()
                .HasColumnName("color_hex");

            entity.HasData(
                new PriorityEntity { Id = 1, Name = "Low", ColorHex = "#10B981", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new PriorityEntity { Id = 2, Name = "Medium", ColorHex = "#F59E0B", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new PriorityEntity { Id = 3, Name = "High", ColorHex = "#EF4444", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );
        });
    }
}

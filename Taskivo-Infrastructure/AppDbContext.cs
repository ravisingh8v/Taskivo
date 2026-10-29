using Microsoft.EntityFrameworkCore;
using Taskivo_Common.Authorization;
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

    public virtual DbSet<RoleEntity> Roles { get; set; }
    public virtual DbSet<PermissionEntity> Permissions { get; set; }
    public virtual DbSet<UserRoleEntity> UserRoles { get; set; }
    public virtual DbSet<RolePermissionEntity> RolePermissions { get; set; }
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

        modelBuilder.Entity<RoleEntity>(entity =>
        {
            entity.ToTable("Roles");

            entity.HasKey(e => e.Id).HasName("Roles_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(500);

            entity.HasIndex(e => e.Name).IsUnique();

            // Initial Role data this might be deleted in future 
            entity.HasData(
                new RoleEntity { Id = RbacSeedIds.AdminRole, Name = "Admin", Description = "Administrator role with full access to all resources." },
                new RoleEntity { Id = RbacSeedIds.UserRole, Name = "User", Description = "Standard user role with limited access to resources." }
            );
        });

        modelBuilder.Entity<PermissionEntity>(entity =>
        {
            entity.ToTable("Permissions");

            entity.HasKey(e => e.Id).HasName("Permissions_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(500);

            entity.HasIndex(e => e.Name).IsUnique();

            entity.HasData(
                new PermissionEntity { Id = RbacSeedIds.TasksRead, Name = PermissionNames.TasksRead, Description = "Permission to read tasks." },
                new PermissionEntity { Id = RbacSeedIds.TasksCreate, Name = PermissionNames.TasksCreate, Description = "Permission to create tasks." },
                new PermissionEntity { Id = RbacSeedIds.TasksUpdate, Name = PermissionNames.TasksUpdate, Description = "Permission to update tasks." },
                new PermissionEntity { Id = RbacSeedIds.TasksDelete, Name = PermissionNames.TasksDelete, Description = "Permission to delete tasks." },
                new PermissionEntity { Id = RbacSeedIds.UsersManage, Name = PermissionNames.UsersManage, Description = "Permission to manage users." },
                new PermissionEntity { Id = RbacSeedIds.RolesManage, Name = PermissionNames.RolesManage, Description = "Permission to manage roles and permissions." }
            );
        });

        modelBuilder.Entity<UserRoleEntity>(entity =>
        {
            entity.ToTable("UserRoles");

            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("UserRoles_pkey");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId).OnDelete(DeleteBehavior.Cascade);

            entity.HasData(
                new UserRoleEntity { UserId = Guid.Parse("050f9135-b2ae-4fab-91c1-cac15141c95f"), RoleId = RbacSeedIds.AdminRole },
                new UserRoleEntity { UserId = Guid.Parse("62bee773-93bd-42f8-8774-a44161e833a0"), RoleId = RbacSeedIds.UserRole }
            );

        });

        modelBuilder.Entity<RolePermissionEntity>(entity =>
        {
            entity.ToTable("RolePermissions");

            entity.HasKey(e => new { e.RoleId, e.PermissionId }).HasName("RolePermissions_pkey");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");

            entity.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Permission).WithMany().HasForeignKey(e => e.PermissionId).OnDelete(DeleteBehavior.Cascade);

            entity.HasData(
                new RolePermissionEntity { RoleId = RbacSeedIds.AdminRole, PermissionId = RbacSeedIds.RolesManage },
                new RolePermissionEntity { RoleId = RbacSeedIds.AdminRole, PermissionId = RbacSeedIds.UsersManage },
                new RolePermissionEntity { RoleId = RbacSeedIds.AdminRole, PermissionId = RbacSeedIds.TasksCreate },
                new RolePermissionEntity { RoleId = RbacSeedIds.AdminRole, PermissionId = RbacSeedIds.TasksRead },
                new RolePermissionEntity { RoleId = RbacSeedIds.AdminRole, PermissionId = RbacSeedIds.TasksUpdate },
                new RolePermissionEntity { RoleId = RbacSeedIds.AdminRole, PermissionId = RbacSeedIds.TasksDelete },
                new RolePermissionEntity { RoleId = RbacSeedIds.UserRole, PermissionId = RbacSeedIds.TasksCreate },
                new RolePermissionEntity { RoleId = RbacSeedIds.UserRole, PermissionId = RbacSeedIds.TasksRead },
                new RolePermissionEntity { RoleId = RbacSeedIds.UserRole, PermissionId = RbacSeedIds.TasksUpdate },
                new RolePermissionEntity { RoleId = RbacSeedIds.UserRole, PermissionId = RbacSeedIds.TasksDelete }
            );
        });


    }

    // RBAC Data Seeding: The following class contains the IDs for the roles and permissions that will be seeded into the database. These IDs are used to establish relationships between roles and permissions.
    internal static class RbacSeedIds
    {
        public static readonly Guid AdminRole =
            Guid.Parse("bb10e887-c4eb-4473-85f8-556838100856");

        public static readonly Guid UserRole =
            Guid.Parse("8542204b-0233-459c-8049-d1a4aca1ff6b");

        public static readonly Guid TasksRead =
            Guid.Parse("4990d90f-c549-4653-a0b8-9ea74ba75154");

        public static readonly Guid TasksCreate =
            Guid.Parse("28ca585b-008c-4c6d-9de5-3cdea4c0ffb3");

        public static readonly Guid TasksUpdate =
            Guid.Parse("bb933aaa-3d53-4777-99ec-6e51bda093df");

        public static readonly Guid TasksDelete =
            Guid.Parse("cf54f7b4-c5d9-426a-a163-de7c8637ce24");

        public static readonly Guid UsersManage =
            Guid.Parse("b436114f-7eb9-44b9-90f6-916de2b2da52");

        public static readonly Guid RolesManage =
            Guid.Parse("304ac39e-a347-4acb-a8c6-d0a2639c4a06");
    }
}

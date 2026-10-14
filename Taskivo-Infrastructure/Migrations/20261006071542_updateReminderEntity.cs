using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskivoInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateReminderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "Reminders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_reminder_at_is_sent_deleted_at",
                table: "Reminders",
                columns: new[] { "reminder_at", "is_sent", "deleted_at" });

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_user_id_reminder_at",
                table: "Reminders",
                columns: new[] { "user_id", "reminder_at" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reminders_reminder_at_is_sent_deleted_at",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_user_id_reminder_at",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Reminders");
        }
    }
}

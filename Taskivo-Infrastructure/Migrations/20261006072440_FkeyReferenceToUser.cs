using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskivoInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FkeyReferenceToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Users_user_id",
                table: "Reminders",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Users_user_id",
                table: "Reminders");
        }
    }
}

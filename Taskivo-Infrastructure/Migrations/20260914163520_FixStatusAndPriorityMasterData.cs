using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskivoInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixStatusAndPriorityMasterData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tasks_priority",
                table: "Tasks",
                column: "priority");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_status",
                table: "Tasks",
                column: "status");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Priorities_priority",
                table: "Tasks",
                column: "priority",
                principalTable: "Priorities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Statuses_status",
                table: "Tasks",
                column: "status",
                principalTable: "Statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Priorities_priority",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Statuses_status",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_priority",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_status",
                table: "Tasks");
        }
    }
}

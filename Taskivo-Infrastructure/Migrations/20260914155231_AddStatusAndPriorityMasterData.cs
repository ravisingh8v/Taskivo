using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskivoInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusAndPriorityMasterData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    color_hex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    color_hex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "id", "color_hex", "name" },
                values: new object[,]
                {
                    { (short)1, "#10B981", "Low" },
                    { (short)2, "#F59E0B", "Medium" },
                    { (short)3, "#EF4444", "High" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "id", "color_hex", "name" },
                values: new object[,]
                {
                    { (short)1, "#2563EB", "New" },
                    { (short)2, "#F59E0B", "In-Progress" },
                    { (short)3, "#A78BFA", "On Hold" },
                    { (short)4, "#22C55E", "Completed" },
                    { (short)5, "#EF4444", "Discarded" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}

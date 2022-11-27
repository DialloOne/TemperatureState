using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SensorState.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusBounds",
                columns: table => new
                {
                    StatusBoundId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpperColdBound = table.Column<int>(type: "int", nullable: false),
                    LowerHotBound = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusBounds", x => x.StatusBoundId);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusBoundId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_Statuses_StatusBounds_StatusBoundId",
                        column: x => x.StatusBoundId,
                        principalTable: "StatusBounds",
                        principalColumn: "StatusBoundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Temperatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temperatures_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_StatusBoundId",
                table: "Statuses",
                column: "StatusBoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Temperatures_StatusId",
                table: "Temperatures",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Temperatures");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "StatusBounds");
        }
    }
}

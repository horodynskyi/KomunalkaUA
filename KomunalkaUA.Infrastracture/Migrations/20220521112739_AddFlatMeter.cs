using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KomunalkaUA.Infrastracture.Migrations
{
    public partial class AddFlatMeter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flats_Meters_MeterId",
                table: "Flats");

            migrationBuilder.DropForeignKey(
                name: "FK_Flats_Meters_MeterId1",
                table: "Flats");

            migrationBuilder.DropForeignKey(
                name: "FK_Flats_Meters_MeterId2",
                table: "Flats");

            migrationBuilder.DropIndex(
                name: "IX_Flats_MeterId",
                table: "Flats");

            migrationBuilder.DropIndex(
                name: "IX_Flats_MeterId1",
                table: "Flats");

            migrationBuilder.DropIndex(
                name: "IX_Flats_MeterId2",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "ElectricMeterId",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "GasMeterId",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "MeterId",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "MeterId1",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "MeterId2",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "WatterMeterId",
                table: "Flats");

            migrationBuilder.CreateTable(
                name: "FlatMeter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FlatId = table.Column<int>(type: "integer", nullable: true),
                    MetterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatMeter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlatMeter_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FlatMeter_Meters_MetterId",
                        column: x => x.MetterId,
                        principalTable: "Meters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlatMeter_FlatId",
                table: "FlatMeter",
                column: "FlatId");

            migrationBuilder.CreateIndex(
                name: "IX_FlatMeter_MetterId",
                table: "FlatMeter",
                column: "MetterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlatMeter");

            migrationBuilder.AddColumn<int>(
                name: "ElectricMeterId",
                table: "Flats",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GasMeterId",
                table: "Flats",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeterId",
                table: "Flats",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeterId1",
                table: "Flats",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeterId2",
                table: "Flats",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WatterMeterId",
                table: "Flats",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flats_MeterId",
                table: "Flats",
                column: "MeterId");

            migrationBuilder.CreateIndex(
                name: "IX_Flats_MeterId1",
                table: "Flats",
                column: "MeterId1");

            migrationBuilder.CreateIndex(
                name: "IX_Flats_MeterId2",
                table: "Flats",
                column: "MeterId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Flats_Meters_MeterId",
                table: "Flats",
                column: "MeterId",
                principalTable: "Meters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flats_Meters_MeterId1",
                table: "Flats",
                column: "MeterId1",
                principalTable: "Meters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flats_Meters_MeterId2",
                table: "Flats",
                column: "MeterId2",
                principalTable: "Meters",
                principalColumn: "Id");
        }
    }
}

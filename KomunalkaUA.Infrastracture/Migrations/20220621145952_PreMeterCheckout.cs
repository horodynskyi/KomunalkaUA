using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KomunalkaUA.Infrastracture.Migrations
{
    public partial class PreMeterCheckout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PreMeterCheckouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FlatId = table.Column<int>(type: "integer", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    EndValue = table.Column<int>(type: "integer", nullable: true),
                    MeterId = table.Column<int>(type: "integer", nullable: true),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreMeterCheckouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreMeterCheckouts_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreMeterCheckouts_Meters_MeterId",
                        column: x => x.MeterId,
                        principalTable: "Meters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreMeterCheckouts_Users_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreMeterCheckouts_FlatId",
                table: "PreMeterCheckouts",
                column: "FlatId");

            migrationBuilder.CreateIndex(
                name: "IX_PreMeterCheckouts_MeterId",
                table: "PreMeterCheckouts",
                column: "MeterId");

            migrationBuilder.CreateIndex(
                name: "IX_PreMeterCheckouts_TenantId",
                table: "PreMeterCheckouts",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreMeterCheckouts");
        }
    }
}

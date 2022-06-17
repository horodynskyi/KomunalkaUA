using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KomunalkaUA.Infrastracture.Migrations
{
    public partial class MeterProviderCheckoutRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkout_Flats_FlatId",
                table: "Checkout");

            migrationBuilder.DropIndex(
                name: "IX_Checkout_FlatId",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "MeterType",
                table: "Meters");

            migrationBuilder.AddColumn<int>(
                name: "MeterTypeId",
                table: "Providers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndValue",
                table: "Checkout",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlatMeterId",
                table: "Checkout",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rent",
                table: "Checkout",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartValue",
                table: "Checkout",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MeterTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MeterTypes",
                columns: new[] { "Id", "DisplayName", "Name" },
                values: new object[,]
                {
                    { 1, null, "Газ" },
                    { 2, null, "Вода" },
                    { 3, null, "Світло" }
                });

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 1,
                column: "MeterTypeId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Providers_MeterTypeId",
                table: "Providers",
                column: "MeterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_FlatMeterId",
                table: "Checkout",
                column: "FlatMeterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkout_FlatMeter_FlatMeterId",
                table: "Checkout",
                column: "FlatMeterId",
                principalTable: "FlatMeter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_MeterTypes_MeterTypeId",
                table: "Providers",
                column: "MeterTypeId",
                principalTable: "MeterTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkout_FlatMeter_FlatMeterId",
                table: "Checkout");

            migrationBuilder.DropForeignKey(
                name: "FK_Providers_MeterTypes_MeterTypeId",
                table: "Providers");

            migrationBuilder.DropTable(
                name: "MeterTypes");

            migrationBuilder.DropIndex(
                name: "IX_Providers_MeterTypeId",
                table: "Providers");

            migrationBuilder.DropIndex(
                name: "IX_Checkout_FlatMeterId",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "MeterTypeId",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "EndValue",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "FlatMeterId",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "Rent",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "StartValue",
                table: "Checkout");

            migrationBuilder.AddColumn<string>(
                name: "MeterType",
                table: "Meters",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_FlatId",
                table: "Checkout",
                column: "FlatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkout_Flats_FlatId",
                table: "Checkout",
                column: "FlatId",
                principalTable: "Flats",
                principalColumn: "Id");
        }
    }
}

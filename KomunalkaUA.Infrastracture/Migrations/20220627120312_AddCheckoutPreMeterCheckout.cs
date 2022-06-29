using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KomunalkaUA.Infrastracture.Migrations
{
    public partial class AddCheckoutPreMeterCheckout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Instant>(
                name: "Date",
                table: "Checkout",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(16563313924386468L),
                oldClrType: typeof(Instant),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: NodaTime.Instant.FromUnixTimeTicks(16563280068604956L));

            migrationBuilder.CreateTable(
                name: "CheckoutPreMeterCheckouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CheckoutId = table.Column<int>(type: "integer", nullable: false),
                    PreMeterCheckoutId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutPreMeterCheckouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutPreMeterCheckouts_Checkout_CheckoutId",
                        column: x => x.CheckoutId,
                        principalTable: "Checkout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckoutPreMeterCheckouts_PreMeterCheckouts_PreMeterCheckou~",
                        column: x => x.PreMeterCheckoutId,
                        principalTable: "PreMeterCheckouts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutPreMeterCheckouts_CheckoutId",
                table: "CheckoutPreMeterCheckouts",
                column: "CheckoutId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutPreMeterCheckouts_PreMeterCheckoutId",
                table: "CheckoutPreMeterCheckouts",
                column: "PreMeterCheckoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckoutPreMeterCheckouts");

            migrationBuilder.AlterColumn<Instant>(
                name: "Date",
                table: "Checkout",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(16563280068604956L),
                oldClrType: typeof(Instant),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: NodaTime.Instant.FromUnixTimeTicks(16563313924386468L));
        }
    }
}

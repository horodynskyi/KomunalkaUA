using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace KomunalkaUA.Infrastracture.Migrations
{
    public partial class RemovedRelationalCheckoutTarifFlatMeter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkout_FlatMeter_FlatMeterId",
                table: "Checkout");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkout_Tariffs_TariffId",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "FlatMeterId",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "TariffId",
                table: "Checkout");

            migrationBuilder.AlterColumn<Instant>(
                name: "Date",
                table: "Checkout",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(16563274925339979L),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Checkout",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(Instant),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: NodaTime.Instant.FromUnixTimeTicks(16563274925339979L));

            migrationBuilder.AddColumn<int>(
                name: "FlatMeterId",
                table: "Checkout",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TariffId",
                table: "Checkout",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkout_FlatMeter_FlatMeterId",
                table: "Checkout",
                column: "FlatMeterId",
                principalTable: "FlatMeter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkout_Tariffs_TariffId",
                table: "Checkout",
                column: "TariffId",
                principalTable: "Tariffs",
                principalColumn: "Id");
        }
    }
}

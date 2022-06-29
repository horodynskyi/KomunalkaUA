using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace KomunalkaUA.Infrastracture.Migrations
{
    public partial class SeedProviders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Instant>(
                name: "Date",
                table: "Checkout",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(16564030219656399L),
                oldClrType: typeof(Instant),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: NodaTime.Instant.FromUnixTimeTicks(16563313924386468L));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Region",
                value: "Чернівецький");

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "Id", "CityId", "MeterTypeId", "Name", "Rate" },
                values: new object[,]
                {
                    { 4, 2, 1, "Газпостач Тернопіль", 7.9900000000000002 },
                    { 5, 2, 3, "Тернопільобленерго", 1.7 },
                    { 6, 2, 2, "Тернопільводоканал", 28.0 },
                    { 7, 2, 1, "Тернопільоблгаз", 7.9900000000000002 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.AlterColumn<Instant>(
                name: "Date",
                table: "Checkout",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(16563313924386468L),
                oldClrType: typeof(Instant),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: NodaTime.Instant.FromUnixTimeTicks(16564030219656399L));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Region",
                value: "Тернопільський");
        }
    }
}

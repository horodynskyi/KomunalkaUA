using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace KomunalkaUA.Infrastracture.Migrations
{
    public partial class AddTwoProvidersSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Instant>(
                name: "Date",
                table: "Checkout",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(16563280068604956L),
                oldClrType: typeof(Instant),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: NodaTime.Instant.FromUnixTimeTicks(16563274925339979L));

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "Id", "CityId", "MeterTypeId", "Name", "Rate" },
                values: new object[,]
                {
                    { 2, 1, 3, "ТОВ 'ЧОЕК'", 1.7 },
                    { 3, 1, 2, "Чернівці Водоканал", 28.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<Instant>(
                name: "Date",
                table: "Checkout",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(16563274925339979L),
                oldClrType: typeof(Instant),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: NodaTime.Instant.FromUnixTimeTicks(16563280068604956L));
        }
    }
}

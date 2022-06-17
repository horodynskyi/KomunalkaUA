using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomunalkaUA.Infrastracture.Migrations
{
    public partial class CityProvideRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "Region" },
                values: new object[,]
                {
                    { 1, "Чернівці", "Чернівецький" },
                    { 2, "Тернопіль", "Тернопільський" }
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "Id", "CityId", "Name", "Rate" },
                values: new object[] { 1, 1, "Нафтогаз", 7.9900000000000002 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomunalkaUA.Infrastracture.Migrations
{
    public partial class AddFlatRent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rent",
                table: "Flats",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rent",
                table: "Flats");
        }
    }
}

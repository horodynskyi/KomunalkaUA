using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomunalkaUA.Infrastracture.Migrations
{
    public partial class AddFlatPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlatPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PhotoId = table.Column<string>(type: "text", nullable: true),
                    FlatId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlatPhotos_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FlatPhotos_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlatPhotos_FlatId",
                table: "FlatPhotos",
                column: "FlatId");

            migrationBuilder.CreateIndex(
                name: "IX_FlatPhotos_PhotoId",
                table: "FlatPhotos",
                column: "PhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlatPhotos");

            migrationBuilder.DropTable(
                name: "Photos");
        }
    }
}

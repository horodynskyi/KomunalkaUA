using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KomunalkaUA.Infrastracture.Migrations
{
    public partial class ChangedCheckoutFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndValue",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "Rent",
                table: "Checkout");

            migrationBuilder.RenameColumn(
                name: "StartValue",
                table: "Checkout",
                newName: "PaymentSum");

            migrationBuilder.AddColumn<int>(
                name: "StartValue",
                table: "PreMeterCheckouts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Checkout",
                type: "timestamp with time zone",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkout_Flats_FlatId",
                table: "Checkout");

            migrationBuilder.DropIndex(
                name: "IX_Checkout_FlatId",
                table: "Checkout");

            migrationBuilder.DropColumn(
                name: "StartValue",
                table: "PreMeterCheckouts");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Checkout");

            migrationBuilder.RenameColumn(
                name: "PaymentSum",
                table: "Checkout",
                newName: "StartValue");

            migrationBuilder.AddColumn<int>(
                name: "EndValue",
                table: "Checkout",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rent",
                table: "Checkout",
                type: "integer",
                nullable: true);
        }
    }
}

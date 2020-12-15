using Microsoft.EntityFrameworkCore.Migrations;

namespace GymBookingSystem.Migrations
{
    public partial class gymupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GymAdress",
                table: "Gyms",
                newName: "StreetAdress");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Gyms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Gyms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Gyms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Gyms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Gyms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Gyms");

            migrationBuilder.RenameColumn(
                name: "StreetAdress",
                table: "Gyms",
                newName: "GymAdress");
        }
    }
}

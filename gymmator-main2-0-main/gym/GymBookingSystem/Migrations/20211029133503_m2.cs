using Microsoft.EntityFrameworkCore.Migrations;

namespace GymBookingSystem.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "LoginCredentials",
                newName: "PasswordHash");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "LoginCredentials",
                newName: "Password");
        }
    }
}

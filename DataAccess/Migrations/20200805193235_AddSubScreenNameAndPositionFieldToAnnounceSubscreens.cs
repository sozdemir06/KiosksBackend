using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddSubScreenNameAndPositionFieldToAnnounceSubscreens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubScreenName",
                table: "VehicleAnnounceSubScreens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubScreenPosition",
                table: "VehicleAnnounceSubScreens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubScreenName",
                table: "HomeAnnounceSubScreens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubScreenPosition",
                table: "HomeAnnounceSubScreens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubScreenName",
                table: "VehicleAnnounceSubScreens");

            migrationBuilder.DropColumn(
                name: "SubScreenPosition",
                table: "VehicleAnnounceSubScreens");

            migrationBuilder.DropColumn(
                name: "SubScreenName",
                table: "HomeAnnounceSubScreens");

            migrationBuilder.DropColumn(
                name: "SubScreenPosition",
                table: "HomeAnnounceSubScreens");
        }
    }
}

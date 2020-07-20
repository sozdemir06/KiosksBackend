using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class RemoveScreenIdFromHomeAnnounce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeAnnounces_Screens_ScreenId",
                table: "HomeAnnounces");

            migrationBuilder.DropIndex(
                name: "IX_HomeAnnounces_ScreenId",
                table: "HomeAnnounces");

            migrationBuilder.DropColumn(
                name: "ScreenId",
                table: "HomeAnnounces");

            migrationBuilder.AddColumn<int>(
                name: "ScreenId",
                table: "HomeAnnounceSubScreens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HomeAnnounceSubScreens_ScreenId",
                table: "HomeAnnounceSubScreens",
                column: "ScreenId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeAnnounceSubScreens_Screens_ScreenId",
                table: "HomeAnnounceSubScreens",
                column: "ScreenId",
                principalTable: "Screens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeAnnounceSubScreens_Screens_ScreenId",
                table: "HomeAnnounceSubScreens");

            migrationBuilder.DropIndex(
                name: "IX_HomeAnnounceSubScreens_ScreenId",
                table: "HomeAnnounceSubScreens");

            migrationBuilder.DropColumn(
                name: "ScreenId",
                table: "HomeAnnounceSubScreens");

            migrationBuilder.AddColumn<int>(
                name: "ScreenId",
                table: "HomeAnnounces",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HomeAnnounces_ScreenId",
                table: "HomeAnnounces",
                column: "ScreenId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeAnnounces_Screens_ScreenId",
                table: "HomeAnnounces",
                column: "ScreenId",
                principalTable: "Screens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

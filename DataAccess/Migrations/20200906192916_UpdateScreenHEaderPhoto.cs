using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class UpdateScreenHEaderPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScreenHeaderPhotos_ScreenHeaders_ScreenHeaderId",
                table: "ScreenHeaderPhotos");

            migrationBuilder.DropIndex(
                name: "IX_ScreenHeaderPhotos_ScreenHeaderId",
                table: "ScreenHeaderPhotos");

            migrationBuilder.DropColumn(
                name: "ScreenHeaderId",
                table: "ScreenHeaderPhotos");

            migrationBuilder.AddColumn<int>(
                name: "ScreenId",
                table: "ScreenHeaderPhotos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ScreenHeaderPhotos_ScreenId",
                table: "ScreenHeaderPhotos",
                column: "ScreenId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenHeaderPhotos_Screens_ScreenId",
                table: "ScreenHeaderPhotos",
                column: "ScreenId",
                principalTable: "Screens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScreenHeaderPhotos_Screens_ScreenId",
                table: "ScreenHeaderPhotos");

            migrationBuilder.DropIndex(
                name: "IX_ScreenHeaderPhotos_ScreenId",
                table: "ScreenHeaderPhotos");

            migrationBuilder.DropColumn(
                name: "ScreenId",
                table: "ScreenHeaderPhotos");

            migrationBuilder.AddColumn<int>(
                name: "ScreenHeaderId",
                table: "ScreenHeaderPhotos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ScreenHeaderPhotos_ScreenHeaderId",
                table: "ScreenHeaderPhotos",
                column: "ScreenHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScreenHeaderPhotos_ScreenHeaders_ScreenHeaderId",
                table: "ScreenHeaderPhotos",
                column: "ScreenHeaderId",
                principalTable: "ScreenHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

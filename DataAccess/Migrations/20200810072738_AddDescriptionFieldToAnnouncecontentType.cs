using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddDescriptionFieldToAnnouncecontentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AnnounceContentTypes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AnnounceContentTypes",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AnnounceContentTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AnnounceContentTypes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

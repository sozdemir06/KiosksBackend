using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddNewsDateAndNewsAgencyFieldToNewsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewsAgency",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NewsDate",
                table: "News",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsAgency",
                table: "News");

            migrationBuilder.DropColumn(
                name: "NewsDate",
                table: "News");
        }
    }
}

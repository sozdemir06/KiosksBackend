using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations
{
    public partial class AddFoodMenuBgPhotoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBackground",
                table: "FoodMenuPhotos");

            migrationBuilder.DropColumn(
                name: "IsSetBackground",
                table: "FoodMenuPhotos");

            migrationBuilder.CreateTable(
                name: "FoodMenuBgPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    FullPath = table.Column<string>(nullable: true),
                    IsSetBackground = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodMenuBgPhotos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodMenuBgPhotos");

            migrationBuilder.AddColumn<bool>(
                name: "IsBackground",
                table: "FoodMenuPhotos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSetBackground",
                table: "FoodMenuPhotos",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

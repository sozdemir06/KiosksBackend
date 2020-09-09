using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations
{
    public partial class AddScreenHeaderFooterEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScreenFooters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FooterText = table.Column<string>(maxLength: 70, nullable: true),
                    IsShowWheatherForCast = table.Column<bool>(nullable: false),
                    IsShowStockExchange = table.Column<bool>(nullable: false),
                    ScreenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenFooters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenFooters_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreenHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HeaderText = table.Column<string>(maxLength: 70, nullable: true),
                    ScreenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenHeaders_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreenHeaderPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    FullPath = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    IsMain = table.Column<bool>(nullable: false),
                    Position = table.Column<string>(nullable: true),
                    ScreenHeaderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenHeaderPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenHeaderPhotos_ScreenHeaders_ScreenHeaderId",
                        column: x => x.ScreenHeaderId,
                        principalTable: "ScreenHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScreenFooters_ScreenId",
                table: "ScreenFooters",
                column: "ScreenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScreenHeaderPhotos_ScreenHeaderId",
                table: "ScreenHeaderPhotos",
                column: "ScreenHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenHeaders_ScreenId",
                table: "ScreenHeaders",
                column: "ScreenId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScreenFooters");

            migrationBuilder.DropTable(
                name: "ScreenHeaderPhotos");

            migrationBuilder.DropTable(
                name: "ScreenHeaders");
        }
    }
}

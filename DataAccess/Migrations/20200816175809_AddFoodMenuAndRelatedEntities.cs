using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations
{
    public partial class AddFoodMenuAndRelatedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodMenus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnnounceType = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    SlideId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    PublishStartDate = table.Column<DateTime>(nullable: false),
                    PublishFinishDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    SlideIntervalTime = table.Column<int>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    Reject = table.Column<bool>(nullable: false),
                    IsPublish = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodMenus_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodMenuPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    FullPath = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    IsConfirm = table.Column<bool>(nullable: false),
                    IsSetBackground = table.Column<bool>(nullable: false),
                    FoodMenuId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodMenuPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodMenuPhotos_FoodMenus_FoodMenuId",
                        column: x => x.FoodMenuId,
                        principalTable: "FoodMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodMenuSubscreens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubScreenId = table.Column<int>(nullable: false),
                    SubScreenName = table.Column<string>(nullable: true),
                    SubScreenPosition = table.Column<string>(nullable: true),
                    FoodMenuId = table.Column<int>(nullable: false),
                    ScreenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodMenuSubscreens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodMenuSubscreens_FoodMenus_FoodMenuId",
                        column: x => x.FoodMenuId,
                        principalTable: "FoodMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodMenuSubscreens_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodMenuSubscreens_SubScreens_SubScreenId",
                        column: x => x.SubScreenId,
                        principalTable: "SubScreens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodMenuPhotos_FoodMenuId",
                table: "FoodMenuPhotos",
                column: "FoodMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodMenus_UserId",
                table: "FoodMenus",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodMenuSubscreens_FoodMenuId",
                table: "FoodMenuSubscreens",
                column: "FoodMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodMenuSubscreens_ScreenId",
                table: "FoodMenuSubscreens",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodMenuSubscreens_SubScreenId",
                table: "FoodMenuSubscreens",
                column: "SubScreenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodMenuPhotos");

            migrationBuilder.DropTable(
                name: "FoodMenuSubscreens");

            migrationBuilder.DropTable(
                name: "FoodMenus");
        }
    }
}

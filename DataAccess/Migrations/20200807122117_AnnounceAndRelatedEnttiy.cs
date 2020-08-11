using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations
{
    public partial class AnnounceAndRelatedEnttiy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnounceContentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnounceContentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Announces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(maxLength: 140, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    AnnounceType = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(maxLength: 30, nullable: false),
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
                    table.PrimaryKey("PK_Announces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announces_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    FullPath = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    IsConfirm = table.Column<bool>(nullable: false),
                    AnnounceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncePhotos_Announces_AnnounceId",
                        column: x => x.AnnounceId,
                        principalTable: "Announces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnounceSubScreens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubScreenId = table.Column<int>(nullable: false),
                    SubScreenName = table.Column<string>(nullable: true),
                    SubScreenPosition = table.Column<string>(nullable: true),
                    AnnounceId = table.Column<int>(nullable: false),
                    ScreenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnounceSubScreens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnounceSubScreens_Announces_AnnounceId",
                        column: x => x.AnnounceId,
                        principalTable: "Announces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnounceSubScreens_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnounceSubScreens_SubScreens_SubScreenId",
                        column: x => x.SubScreenId,
                        principalTable: "SubScreens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncePhotos_AnnounceId",
                table: "AnnouncePhotos",
                column: "AnnounceId");

            migrationBuilder.CreateIndex(
                name: "IX_Announces_UserId",
                table: "Announces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnounceSubScreens_AnnounceId",
                table: "AnnounceSubScreens",
                column: "AnnounceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnounceSubScreens_ScreenId",
                table: "AnnounceSubScreens",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnounceSubScreens_SubScreenId",
                table: "AnnounceSubScreens",
                column: "SubScreenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnounceContentTypes");

            migrationBuilder.DropTable(
                name: "AnnouncePhotos");

            migrationBuilder.DropTable(
                name: "AnnounceSubScreens");

            migrationBuilder.DropTable(
                name: "Announces");
        }
    }
}

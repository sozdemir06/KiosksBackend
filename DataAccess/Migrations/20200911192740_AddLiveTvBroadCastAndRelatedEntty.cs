using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations
{
    public partial class AddLiveTvBroadCastAndRelatedEntty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LiveTvBroadCasts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(maxLength: 140, nullable: false),
                    AnnounceType = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    YoutubeId = table.Column<string>(nullable: false),
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
                    table.PrimaryKey("PK_LiveTvBroadCasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiveTvBroadCasts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LiveTvLists",
                columns: table => new
                {
                    YoutubeId = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    TvName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveTvLists", x => x.YoutubeId);
                });

            migrationBuilder.CreateTable(
                name: "LiveTvBroadCastSubScreens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubScreenId = table.Column<int>(nullable: false),
                    SubScreenName = table.Column<string>(nullable: true),
                    SubScreenPosition = table.Column<string>(nullable: true),
                    LiveTvBroadCastId = table.Column<int>(nullable: false),
                    ScreenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveTvBroadCastSubScreens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiveTvBroadCastSubScreens_LiveTvBroadCasts_LiveTvBroadCastId",
                        column: x => x.LiveTvBroadCastId,
                        principalTable: "LiveTvBroadCasts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiveTvBroadCastSubScreens_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiveTvBroadCastSubScreens_SubScreens_SubScreenId",
                        column: x => x.SubScreenId,
                        principalTable: "SubScreens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiveTvBroadCasts_UserId",
                table: "LiveTvBroadCasts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTvBroadCastSubScreens_LiveTvBroadCastId",
                table: "LiveTvBroadCastSubScreens",
                column: "LiveTvBroadCastId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTvBroadCastSubScreens_ScreenId",
                table: "LiveTvBroadCastSubScreens",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTvBroadCastSubScreens_SubScreenId",
                table: "LiveTvBroadCastSubScreens",
                column: "SubScreenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LiveTvBroadCastSubScreens");

            migrationBuilder.DropTable(
                name: "LiveTvLists");

            migrationBuilder.DropTable(
                name: "LiveTvBroadCasts");
        }
    }
}

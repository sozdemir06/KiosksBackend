using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations
{
    public partial class AddHomeAnnounceAndRelatedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomeAnnounces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(maxLength: 140, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    AnnounceType = table.Column<string>(nullable: true),
                    SlideId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    PublishStartDate = table.Column<DateTime>(nullable: false),
                    PublishFinishDate = table.Column<DateTime>(nullable: false),
                    NumberOfRoomId = table.Column<int>(nullable: false),
                    HeatingTypeId = table.Column<int>(nullable: false),
                    FlatOfHomeId = table.Column<int>(nullable: false),
                    ScreenId = table.Column<int>(nullable: false),
                    BuildingAgeId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    SquareMeters = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    Reject = table.Column<bool>(nullable: false),
                    IsPublish = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeAnnounces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeAnnounces_BuildingsAge_BuildingAgeId",
                        column: x => x.BuildingAgeId,
                        principalTable: "BuildingsAge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeAnnounces_FlatsOfHome_FlatOfHomeId",
                        column: x => x.FlatOfHomeId,
                        principalTable: "FlatsOfHome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeAnnounces_HeatingTypes_HeatingTypeId",
                        column: x => x.HeatingTypeId,
                        principalTable: "HeatingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeAnnounces_NumberOfRooms_NumberOfRoomId",
                        column: x => x.NumberOfRoomId,
                        principalTable: "NumberOfRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeAnnounces_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeAnnounces_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeAnnouncePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    FullPath = table.Column<string>(nullable: true),
                    IsConfirm = table.Column<bool>(nullable: false),
                    HomeAnnounceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeAnnouncePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeAnnouncePhotos_HomeAnnounces_HomeAnnounceId",
                        column: x => x.HomeAnnounceId,
                        principalTable: "HomeAnnounces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeAnnounceSubScreens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubScreenId = table.Column<int>(nullable: false),
                    HomeAnnounceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeAnnounceSubScreens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeAnnounceSubScreens_HomeAnnounces_HomeAnnounceId",
                        column: x => x.HomeAnnounceId,
                        principalTable: "HomeAnnounces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeAnnounceSubScreens_SubScreens_SubScreenId",
                        column: x => x.SubScreenId,
                        principalTable: "SubScreens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeAnnouncePhotos_HomeAnnounceId",
                table: "HomeAnnouncePhotos",
                column: "HomeAnnounceId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeAnnounces_BuildingAgeId",
                table: "HomeAnnounces",
                column: "BuildingAgeId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeAnnounces_FlatOfHomeId",
                table: "HomeAnnounces",
                column: "FlatOfHomeId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeAnnounces_HeatingTypeId",
                table: "HomeAnnounces",
                column: "HeatingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeAnnounces_NumberOfRoomId",
                table: "HomeAnnounces",
                column: "NumberOfRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeAnnounces_ScreenId",
                table: "HomeAnnounces",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeAnnounces_UserId",
                table: "HomeAnnounces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeAnnounceSubScreens_HomeAnnounceId",
                table: "HomeAnnounceSubScreens",
                column: "HomeAnnounceId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeAnnounceSubScreens_SubScreenId",
                table: "HomeAnnounceSubScreens",
                column: "SubScreenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeAnnouncePhotos");

            migrationBuilder.DropTable(
                name: "HomeAnnounceSubScreens");

            migrationBuilder.DropTable(
                name: "HomeAnnounces");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations
{
    public partial class VehicleAnnounceAndRelatedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleAnnounces",
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
                    VehicleCategoryId = table.Column<int>(nullable: false),
                    VehicleBrandId = table.Column<int>(nullable: false),
                    VehicleModelId = table.Column<int>(nullable: false),
                    VehicleFuelTypeId = table.Column<int>(nullable: false),
                    VehicleGearTypeId = table.Column<int>(nullable: false),
                    VehicleEngineSizeId = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    SquareMeters = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    Reject = table.Column<bool>(nullable: false),
                    IsPublish = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleAnnounces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleAnnounces_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleAnnounces_VehicleBrands_VehicleBrandId",
                        column: x => x.VehicleBrandId,
                        principalTable: "VehicleBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleAnnounces_VehicleCategories_VehicleCategoryId",
                        column: x => x.VehicleCategoryId,
                        principalTable: "VehicleCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleAnnounces_VehicleEngineSizes_VehicleEngineSizeId",
                        column: x => x.VehicleEngineSizeId,
                        principalTable: "VehicleEngineSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleAnnounces_VehicleFuelTypes_VehicleFuelTypeId",
                        column: x => x.VehicleFuelTypeId,
                        principalTable: "VehicleFuelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleAnnounces_VehicleGearTypes_VehicleGearTypeId",
                        column: x => x.VehicleGearTypeId,
                        principalTable: "VehicleGearTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleAnnounces_VehicleModels_VehicleModelId",
                        column: x => x.VehicleModelId,
                        principalTable: "VehicleModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleAnnouncePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    FullPath = table.Column<string>(nullable: true),
                    IsConfirm = table.Column<bool>(nullable: false),
                    VehicleAnnounceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleAnnouncePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleAnnouncePhotos_VehicleAnnounces_VehicleAnnounceId",
                        column: x => x.VehicleAnnounceId,
                        principalTable: "VehicleAnnounces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleAnnounceSubScreens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubScreenId = table.Column<int>(nullable: false),
                    VehicleAnnounceId = table.Column<int>(nullable: false),
                    ScreenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleAnnounceSubScreens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleAnnounceSubScreens_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleAnnounceSubScreens_SubScreens_SubScreenId",
                        column: x => x.SubScreenId,
                        principalTable: "SubScreens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleAnnounceSubScreens_VehicleAnnounces_VehicleAnnounceId",
                        column: x => x.VehicleAnnounceId,
                        principalTable: "VehicleAnnounces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAnnouncePhotos_VehicleAnnounceId",
                table: "VehicleAnnouncePhotos",
                column: "VehicleAnnounceId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAnnounces_UserId",
                table: "VehicleAnnounces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAnnounces_VehicleBrandId",
                table: "VehicleAnnounces",
                column: "VehicleBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAnnounces_VehicleCategoryId",
                table: "VehicleAnnounces",
                column: "VehicleCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAnnounces_VehicleEngineSizeId",
                table: "VehicleAnnounces",
                column: "VehicleEngineSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAnnounces_VehicleFuelTypeId",
                table: "VehicleAnnounces",
                column: "VehicleFuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAnnounces_VehicleGearTypeId",
                table: "VehicleAnnounces",
                column: "VehicleGearTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAnnounces_VehicleModelId",
                table: "VehicleAnnounces",
                column: "VehicleModelId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAnnounceSubScreens_ScreenId",
                table: "VehicleAnnounceSubScreens",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAnnounceSubScreens_SubScreenId",
                table: "VehicleAnnounceSubScreens",
                column: "SubScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAnnounceSubScreens_VehicleAnnounceId",
                table: "VehicleAnnounceSubScreens",
                column: "VehicleAnnounceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleAnnouncePhotos");

            migrationBuilder.DropTable(
                name: "VehicleAnnounceSubScreens");

            migrationBuilder.DropTable(
                name: "VehicleAnnounces");
        }
    }
}

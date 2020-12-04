using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnounceContentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'40', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnounceContentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuildingsAge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingsAge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 140, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Selected = table.Column<bool>(nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    ShorName = table.Column<string>(nullable: true),
                    Selected = table.Column<bool>(nullable: false),
                    Symbol = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 140, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 140, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlatsOfHome",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatsOfHome", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "HeatingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeatingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LiveTvLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    YoutubeId = table.Column<string>(nullable: true),
                    TvName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveTvLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LogDetail = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Audit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotifyGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifyGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NumberOfRooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberOfRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicFooterTexts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FooterText = table.Column<string>(nullable: true),
                    ContentPhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicFooterTexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicLogos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    FullPath = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    IsMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicLogos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 60, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Screens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'10', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: true),
                    IsFull = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'5', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleEngineSizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleEngineSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleFuelTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleFuelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleGearTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleGearTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(maxLength: 25, nullable: true),
                    LastName = table.Column<string>(maxLength: 30, nullable: true),
                    InterPhone = table.Column<string>(maxLength: 11, nullable: true),
                    GsmPhone = table.Column<string>(maxLength: 11, nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    CampusId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    DegreeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Campuses_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Degrees_DegreeId",
                        column: x => x.DegreeId,
                        principalTable: "Degrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 60, nullable: true),
                    Description = table.Column<string>(maxLength: 140, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    RoleCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_RoleCategories_RoleCategoryId",
                        column: x => x.RoleCategoryId,
                        principalTable: "RoleCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnlineScreens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConnectionId = table.Column<string>(nullable: true),
                    ScreenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineScreens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnlineScreens_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    ScreenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenHeaderPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenHeaderPhotos_Screens_ScreenId",
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
                name: "SubScreens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Position = table.Column<string>(maxLength: 30, nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    ScreenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubScreens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubScreens_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleBrands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrandName = table.Column<string>(maxLength: 60, nullable: false),
                    VehicleCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleBrands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleBrands_VehicleCategories_VehicleCategoryId",
                        column: x => x.VehicleCategoryId,
                        principalTable: "VehicleCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Announces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(maxLength: 140, nullable: false),
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
                    BuildingAgeId = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    SquareMeters = table.Column<int>(nullable: false),
                    SlideIntervalTime = table.Column<int>(nullable: false),
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
                        name: "FK_HomeAnnounces_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(maxLength: 140, nullable: false),
                    AnnounceType = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(maxLength: 30, nullable: false),
                    SlideId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    PublishStartDate = table.Column<DateTime>(nullable: false),
                    PublishFinishDate = table.Column<DateTime>(nullable: false),
                    NewsDate = table.Column<DateTime>(nullable: false),
                    NewsAgency = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    SlideIntervalTime = table.Column<int>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    Reject = table.Column<bool>(nullable: false),
                    IsPublish = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNotifyGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    NotifyGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifyGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotifyGroups_NotifyGroups_NotifyGroupId",
                        column: x => x.NotifyGroupId,
                        principalTable: "NotifyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserNotifyGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    FullPath = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    IsConfirm = table.Column<bool>(nullable: false),
                    UnConfirm = table.Column<bool>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPhotos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'825', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VehicleModelName = table.Column<string>(maxLength: 60, nullable: false),
                    VehicleBrandId = table.Column<int>(nullable: false),
                    VehicleCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleModels_VehicleBrands_VehicleBrandId",
                        column: x => x.VehicleBrandId,
                        principalTable: "VehicleBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleModels_VehicleCategories_VehicleCategoryId",
                        column: x => x.VehicleCategoryId,
                        principalTable: "VehicleCategories",
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
                    UnConfirm = table.Column<bool>(nullable: false),
                    AnnounceId = table.Column<int>(nullable: false),
                    Duration = table.Column<int>(nullable: false)
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
                    UnConfirm = table.Column<bool>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "HomeAnnouncePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    FullPath = table.Column<string>(nullable: true),
                    IsConfirm = table.Column<bool>(nullable: false),
                    UnConfirm = table.Column<bool>(nullable: false),
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
                    SubScreenName = table.Column<string>(nullable: true),
                    SubScreenPosition = table.Column<string>(nullable: true),
                    HomeAnnounceId = table.Column<int>(nullable: false),
                    ScreenId = table.Column<int>(nullable: false)
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
                        name: "FK_HomeAnnounceSubScreens_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeAnnounceSubScreens_SubScreens_SubScreenId",
                        column: x => x.SubScreenId,
                        principalTable: "SubScreens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "NewsPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    FullPath = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    IsConfirm = table.Column<bool>(nullable: false),
                    UnConfirm = table.Column<bool>(nullable: false),
                    NewsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsPhotos_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsSubScreens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubScreenId = table.Column<int>(nullable: false),
                    SubScreenName = table.Column<string>(nullable: true),
                    SubScreenPosition = table.Column<string>(nullable: true),
                    NewsId = table.Column<int>(nullable: false),
                    ScreenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsSubScreens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsSubScreens_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsSubScreens_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsSubScreens_SubScreens_SubScreenId",
                        column: x => x.SubScreenId,
                        principalTable: "SubScreens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    SlideIntervalTime = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
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
                    UnConfirm = table.Column<bool>(nullable: false),
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
                    SubScreenName = table.Column<string>(nullable: true),
                    SubScreenPosition = table.Column<string>(nullable: true),
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
                name: "IX_HomeAnnounces_UserId",
                table: "HomeAnnounces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeAnnounceSubScreens_HomeAnnounceId",
                table: "HomeAnnounceSubScreens",
                column: "HomeAnnounceId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeAnnounceSubScreens_ScreenId",
                table: "HomeAnnounceSubScreens",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeAnnounceSubScreens_SubScreenId",
                table: "HomeAnnounceSubScreens",
                column: "SubScreenId");

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

            migrationBuilder.CreateIndex(
                name: "IX_News_UserId",
                table: "News",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsPhotos_NewsId",
                table: "NewsPhotos",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsSubScreens_NewsId",
                table: "NewsSubScreens",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsSubScreens_ScreenId",
                table: "NewsSubScreens",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsSubScreens_SubScreenId",
                table: "NewsSubScreens",
                column: "SubScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineScreens_ScreenId",
                table: "OnlineScreens",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleCategoryId",
                table: "Roles",
                column: "RoleCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenFooters_ScreenId",
                table: "ScreenFooters",
                column: "ScreenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScreenHeaderPhotos_ScreenId",
                table: "ScreenHeaderPhotos",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenHeaders_ScreenId",
                table: "ScreenHeaders",
                column: "ScreenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubScreens_ScreenId",
                table: "SubScreens",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifyGroups_NotifyGroupId",
                table: "UserNotifyGroups",
                column: "NotifyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifyGroups_UserId",
                table: "UserNotifyGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhotos_UserId",
                table: "UserPhotos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CampusId",
                table: "Users",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DegreeId",
                table: "Users",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

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

            migrationBuilder.CreateIndex(
                name: "IX_VehicleBrands_VehicleCategoryId",
                table: "VehicleBrands",
                column: "VehicleCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_VehicleBrandId",
                table: "VehicleModels",
                column: "VehicleBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_VehicleCategoryId",
                table: "VehicleModels",
                column: "VehicleCategoryId");
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
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "FoodMenuBgPhotos");

            migrationBuilder.DropTable(
                name: "FoodMenuPhotos");

            migrationBuilder.DropTable(
                name: "FoodMenuSubscreens");

            migrationBuilder.DropTable(
                name: "HomeAnnouncePhotos");

            migrationBuilder.DropTable(
                name: "HomeAnnounceSubScreens");

            migrationBuilder.DropTable(
                name: "LiveTvBroadCastSubScreens");

            migrationBuilder.DropTable(
                name: "LiveTvLists");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "NewsPhotos");

            migrationBuilder.DropTable(
                name: "NewsSubScreens");

            migrationBuilder.DropTable(
                name: "OnlineScreens");

            migrationBuilder.DropTable(
                name: "PublicFooterTexts");

            migrationBuilder.DropTable(
                name: "PublicLogos");

            migrationBuilder.DropTable(
                name: "ScreenFooters");

            migrationBuilder.DropTable(
                name: "ScreenHeaderPhotos");

            migrationBuilder.DropTable(
                name: "ScreenHeaders");

            migrationBuilder.DropTable(
                name: "UserNotifyGroups");

            migrationBuilder.DropTable(
                name: "UserPhotos");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "VehicleAnnouncePhotos");

            migrationBuilder.DropTable(
                name: "VehicleAnnounceSubScreens");

            migrationBuilder.DropTable(
                name: "Announces");

            migrationBuilder.DropTable(
                name: "FoodMenus");

            migrationBuilder.DropTable(
                name: "HomeAnnounces");

            migrationBuilder.DropTable(
                name: "LiveTvBroadCasts");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "NotifyGroups");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "SubScreens");

            migrationBuilder.DropTable(
                name: "VehicleAnnounces");

            migrationBuilder.DropTable(
                name: "BuildingsAge");

            migrationBuilder.DropTable(
                name: "FlatsOfHome");

            migrationBuilder.DropTable(
                name: "HeatingTypes");

            migrationBuilder.DropTable(
                name: "NumberOfRooms");

            migrationBuilder.DropTable(
                name: "RoleCategories");

            migrationBuilder.DropTable(
                name: "Screens");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VehicleEngineSizes");

            migrationBuilder.DropTable(
                name: "VehicleFuelTypes");

            migrationBuilder.DropTable(
                name: "VehicleGearTypes");

            migrationBuilder.DropTable(
                name: "VehicleModels");

            migrationBuilder.DropTable(
                name: "Campuses");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "VehicleBrands");

            migrationBuilder.DropTable(
                name: "VehicleCategories");
        }
    }
}

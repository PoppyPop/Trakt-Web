using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TraktDl.Business.Database.SqLite.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApiData = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlackListShows",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TraktShowId = table.Column<uint>(nullable: false),
                    Entire = table.Column<bool>(nullable: false),
                    SerieName = table.Column<string>(nullable: true),
                    Season = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackListShows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false),
                    Blacklisted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: true),
                    PosterUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ShowID = table.Column<uint>(nullable: false),
                    SeasonNumber = table.Column<int>(nullable: false),
                    Blacklisted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_Shows_ShowID",
                        column: x => x.ShowID,
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SeasonID = table.Column<Guid>(nullable: false),
                    EpisodeNumber = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PosterUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_Seasons_SeasonID",
                        column: x => x.SeasonID,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlackListShows_TraktShowId_Season",
                table: "BlackListShows",
                columns: new[] { "TraktShowId", "Season" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_SeasonID_EpisodeNumber",
                table: "Episodes",
                columns: new[] { "SeasonID", "EpisodeNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_ShowID_SeasonNumber",
                table: "Seasons",
                columns: new[] { "ShowID", "SeasonNumber" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiKeys");

            migrationBuilder.DropTable(
                name: "BlackListShows");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Shows");
        }
    }
}

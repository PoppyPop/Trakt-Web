using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TraktDl.Business.Database.SqLite.Migrations
{
    public partial class Add_ShowSeasonEpisode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Show",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TraktShowId = table.Column<uint>(nullable: false),
                    Blacklisted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PosterUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Show", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ShowID = table.Column<Guid>(nullable: false),
                    SeasonNumber = table.Column<uint>(nullable: false),
                    Blacklisted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Season_Show_ShowID",
                        column: x => x.ShowID,
                        principalTable: "Show",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episode",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SeasonID = table.Column<Guid>(nullable: false),
                    EpisodeNumber = table.Column<uint>(nullable: false),
                    Blacklisted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PosterUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episode_Season_SeasonID",
                        column: x => x.SeasonID,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Episode_SeasonID_EpisodeNumber",
                table: "Episode",
                columns: new[] { "SeasonID", "EpisodeNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Season_ShowID_SeasonNumber",
                table: "Season",
                columns: new[] { "ShowID", "SeasonNumber" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Episode");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "Show");
        }
    }
}

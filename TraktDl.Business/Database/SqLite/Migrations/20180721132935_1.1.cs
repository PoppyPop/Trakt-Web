using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TraktDl.Business.Database.SqLite.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackListShows");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlackListShows",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Entire = table.Column<bool>(nullable: false),
                    Season = table.Column<int>(nullable: true),
                    SerieName = table.Column<string>(nullable: true),
                    TraktShowId = table.Column<uint>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackListShows", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlackListShows_TraktShowId_Season",
                table: "BlackListShows",
                columns: new[] { "TraktShowId", "Season" },
                unique: true);
        }
    }
}

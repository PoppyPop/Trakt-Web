using Microsoft.EntityFrameworkCore.Migrations;

namespace TraktDl.Business.Database.SqLite.Migrations
{
    public partial class Add_ApiKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episode_Season_SeasonID",
                table: "Episode");

            migrationBuilder.DropForeignKey(
                name: "FK_Season_Show_ShowID",
                table: "Season");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Show",
                table: "Show");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Season",
                table: "Season");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Episode",
                table: "Episode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlackListShow",
                table: "BlackListShow");

            migrationBuilder.RenameTable(
                name: "Show",
                newName: "Shows");

            migrationBuilder.RenameTable(
                name: "Season",
                newName: "Seasons");

            migrationBuilder.RenameTable(
                name: "Episode",
                newName: "Episodes");

            migrationBuilder.RenameTable(
                name: "BlackListShow",
                newName: "BlackListShows");

            migrationBuilder.RenameIndex(
                name: "IX_Season_ShowID_SeasonNumber",
                table: "Seasons",
                newName: "IX_Seasons_ShowID_SeasonNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Episode_SeasonID_EpisodeNumber",
                table: "Episodes",
                newName: "IX_Episodes_SeasonID_EpisodeNumber");

            migrationBuilder.RenameIndex(
                name: "IX_BlackListShow_TraktShowId_Season",
                table: "BlackListShows",
                newName: "IX_BlackListShows_TraktShowId_Season");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shows",
                table: "Shows",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Episodes",
                table: "Episodes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlackListShows",
                table: "BlackListShows",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Seasons_SeasonID",
                table: "Episodes",
                column: "SeasonID",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_Shows_ShowID",
                table: "Seasons",
                column: "ShowID",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Seasons_SeasonID",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_Shows_ShowID",
                table: "Seasons");

            migrationBuilder.DropTable(
                name: "ApiKeys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shows",
                table: "Shows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Episodes",
                table: "Episodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlackListShows",
                table: "BlackListShows");

            migrationBuilder.RenameTable(
                name: "Shows",
                newName: "Show");

            migrationBuilder.RenameTable(
                name: "Seasons",
                newName: "Season");

            migrationBuilder.RenameTable(
                name: "Episodes",
                newName: "Episode");

            migrationBuilder.RenameTable(
                name: "BlackListShows",
                newName: "BlackListShow");

            migrationBuilder.RenameIndex(
                name: "IX_Seasons_ShowID_SeasonNumber",
                table: "Season",
                newName: "IX_Season_ShowID_SeasonNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Episodes_SeasonID_EpisodeNumber",
                table: "Episode",
                newName: "IX_Episode_SeasonID_EpisodeNumber");

            migrationBuilder.RenameIndex(
                name: "IX_BlackListShows_TraktShowId_Season",
                table: "BlackListShow",
                newName: "IX_BlackListShow_TraktShowId_Season");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Show",
                table: "Show",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Season",
                table: "Season",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Episode",
                table: "Episode",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlackListShow",
                table: "BlackListShow",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Episode_Season_SeasonID",
                table: "Episode",
                column: "SeasonID",
                principalTable: "Season",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Season_Show_ShowID",
                table: "Season",
                column: "ShowID",
                principalTable: "Show",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

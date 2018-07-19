using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.AutoDl.Database.SqLite.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Entire",
                table: "BlackListShow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_BlackListShow_TraktShowId_Season",
                table: "BlackListShow",
                columns: new[] { "TraktShowId", "Season" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BlackListShow_TraktShowId_Season",
                table: "BlackListShow");

            migrationBuilder.DropColumn(
                name: "Entire",
                table: "BlackListShow");
        }
    }
}

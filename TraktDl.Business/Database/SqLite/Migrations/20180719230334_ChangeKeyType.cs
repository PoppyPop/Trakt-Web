using Microsoft.EntityFrameworkCore.Migrations;

namespace TraktDl.Business.Database.SqLite.Migrations
{
    public partial class ChangeKeyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProvidersData",
                table: "Shows",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvidersData",
                table: "Episodes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProvidersData",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "ProvidersData",
                table: "Episodes");
        }
    }
}

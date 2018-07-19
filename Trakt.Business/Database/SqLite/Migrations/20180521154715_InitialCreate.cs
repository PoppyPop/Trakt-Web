using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.AutoDl.Database.SqLite.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlackListShow",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TraktShowId = table.Column<uint>(nullable: false),
                    SerieName = table.Column<string>(nullable: true),
                    Season = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackListShow", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackListShow");
        }
    }
}

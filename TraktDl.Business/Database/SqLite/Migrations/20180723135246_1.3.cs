using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TraktDl.Business.Database.SqLite.Migrations
{
    public partial class _13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AirDate",
                table: "Episodes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirDate",
                table: "Episodes");
        }
    }
}

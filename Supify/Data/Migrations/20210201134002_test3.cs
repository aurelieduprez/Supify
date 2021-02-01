using Microsoft.EntityFrameworkCore.Migrations;

namespace Supify.Data.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Playlist",
                columns: new[] { "Id", "Name", "User" },
                values: new object[] { 3, "All time favorites", "999" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Playlist",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}

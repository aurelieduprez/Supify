using Microsoft.EntityFrameworkCore.Migrations;

namespace Supify.Data.Migrations
{
    public partial class test666 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Song",
                columns: new[] { "Id", "Name", "Path", "PlaylistId" },
                values: new object[] { 3, "the Weeknd - Save your Tears", null, 3 });

            migrationBuilder.InsertData(
                table: "Song",
                columns: new[] { "Id", "Name", "Path", "PlaylistId" },
                values: new object[] { 4, "Yazoo - Only You", null, 3 });

            migrationBuilder.InsertData(
                table: "Song",
                columns: new[] { "Id", "Name", "Path", "PlaylistId" },
                values: new object[] { 5, "JOJI - DANCING IN THE DARK", null, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class ICMODaily_AddColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FItemID",
                table: "ICMODaily",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FItemModel",
                table: "ICMODaily",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FItemName",
                table: "ICMODaily",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FItemID",
                table: "ICMODaily");

            migrationBuilder.DropColumn(
                name: "FItemModel",
                table: "ICMODaily");

            migrationBuilder.DropColumn(
                name: "FItemName",
                table: "ICMODaily");
        }
    }
}

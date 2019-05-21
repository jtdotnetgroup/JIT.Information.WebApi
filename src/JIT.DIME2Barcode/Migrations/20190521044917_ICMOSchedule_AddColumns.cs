using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class ICMOSchedule_AddColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "FItemID",
                table: "ICMOSchedule",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FItemModel",
                table: "ICMOSchedule",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FItemName",
                table: "ICMOSchedule",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FItemID",
                table: "ICMOSchedule");

            migrationBuilder.DropColumn(
                name: "FItemModel",
                table: "ICMOSchedule");

            migrationBuilder.DropColumn(
                name: "FItemName",
                table: "ICMOSchedule");

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
    }
}

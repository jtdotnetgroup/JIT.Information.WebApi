using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class Modify_Entity_icmoschedul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           


            migrationBuilder.AddColumn<string>(
                name: "ICMOInspectBillID",
                table: "ICMOQualityRpt",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FItemID",
                table: "ICMOSchedule",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FItemNumber",
                table: "ICMOSchedule",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}

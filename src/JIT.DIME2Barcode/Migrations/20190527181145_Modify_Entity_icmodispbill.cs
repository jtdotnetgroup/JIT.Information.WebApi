using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class Modify_Entity_icmodispbill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<int>(
                name: "FWorker",
                table: "ICMODispBill",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FShiftID",
                table: "VW_DispatchBill_List");

            migrationBuilder.DropColumn(
                name: "FWorkerID",
                table: "VW_DispatchBill_List");

            migrationBuilder.AlterColumn<int>(
                name: "FShift",
                table: "VW_DispatchBill_List",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FWorker",
                table: "ICMODispBill",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}

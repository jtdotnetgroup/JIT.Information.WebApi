using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class Modify_Entity_icmodaily : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FWorker",
                table: "ICMODaily",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VW_Group_ICMODaily");

           
        }
    }
}

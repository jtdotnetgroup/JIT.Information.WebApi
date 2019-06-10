using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class Entity_ICMODispBill_Add_Field_FStartTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<DateTime>(
                name: "FStartTime",
                table: "ICMODispBill",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "F_102",
                table: "VW_MODispBillList");

            migrationBuilder.DropColumn(
                name: "FYSQty",
                table: "ICMOInspectBill");

            migrationBuilder.DropColumn(
                name: "FStartTime",
                table: "ICMODispBill");

            migrationBuilder.AlterColumn<int>(
                name: "FWorkshopType",
                table: "t_Organizationunit",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class Modify_Entity_Equipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FSwichTime",
                table: "t_equipment",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "FMaxWorkHours",
                table: "t_equipment",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FDayWorkHours",
                table: "t_equipment",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_T_OrganizationUnit_FDepartment",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_T_EquimentShift_Employee_FEmployeeID",
                table: "T_EquimentShift");

            migrationBuilder.DropForeignKey(
                name: "FK_T_EquimentShift_t_equipment_FEqiupmentID",
                table: "T_EquimentShift");

            migrationBuilder.DropTable(
                name: "VW_Employee");

            migrationBuilder.DropIndex(
                name: "IX_T_EquimentShift_FEmployeeID",
                table: "T_EquimentShift");

            migrationBuilder.DropIndex(
                name: "IX_T_EquimentShift_FEqiupmentID",
                table: "T_EquimentShift");

            migrationBuilder.DropIndex(
                name: "IX_Employee_FDepartment",
                table: "Employee");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FSwichTime",
                table: "t_equipment",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "FMaxWorkHours",
                table: "t_equipment",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "FDayWorkHours",
                table: "t_equipment",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}

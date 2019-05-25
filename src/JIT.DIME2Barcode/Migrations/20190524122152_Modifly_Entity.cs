using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class Modifly_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FLift",
                table: "t_equipment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FResidualLife",
                table: "t_equipment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "FRunsRate",
                table: "t_equipment",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "FSwichTime",
                table: "t_equipment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FTimeUnit",
                table: "t_equipment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FLift",
                table: "t_equipment");

            migrationBuilder.DropColumn(
                name: "FResidualLife",
                table: "t_equipment");

            migrationBuilder.DropColumn(
                name: "FRunsRate",
                table: "t_equipment");

            migrationBuilder.DropColumn(
                name: "FSwichTime",
                table: "t_equipment");

            migrationBuilder.DropColumn(
                name: "FTimeUnit",
                table: "t_equipment");
        }
    }
}

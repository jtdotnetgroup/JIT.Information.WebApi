using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FMpno = table.Column<string>(maxLength: 100, nullable: true),
                    FName = table.Column<string>(maxLength: 10, nullable: true),
                    FSex = table.Column<int>(nullable: true),
                    FDepartment = table.Column<int>(nullable: true),
                    FWorkingState = table.Column<int>(nullable: true),
                    FSystemUser = table.Column<int>(nullable: true),
                    FParentId = table.Column<int>(nullable: true),
                    FPhone = table.Column<string>(nullable: true),
                    FHiredate = table.Column<DateTime>(nullable: true),
                    FEmailAddress = table.Column<string>(nullable: true),
                    FERPUser = table.Column<int>(nullable: true),
                    FERPOfficeClerk = table.Column<int>(nullable: true),
                    FTenantId = table.Column<int>(nullable: false),
                    FOrganizationUnitId = table.Column<int>(nullable: false),
                    FUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.DropTable(
                name: "Employee");

           
        }
    }
}

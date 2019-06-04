using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class Meger_ZHL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Employee_Organizationunit_FDepartment",
            //    table: "Employee");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_t_equipment_Organizationunit_FWorkCenterID",
            //    table: "t_equipment");

            //migrationBuilder.DropTable(
            //    name: "Organizationunit");

            migrationBuilder.AlterColumn<bool>(
                name: "FDeleted",
                table: "TB_BadItemRelation",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "ICMODispBillID",
            //    table: "ICMOInspectBill",
            //    nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "t_Organizationunit",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        Code = table.Column<string>(maxLength: 100, nullable: false),
            //        CreationTime = table.Column<DateTime>(nullable: true),
            //        CreatorUserId = table.Column<long>(nullable: true),
            //        DeleterUserId = table.Column<long>(nullable: true),
            //        DeletionTime = table.Column<DateTime>(nullable: true),
            //        DisplayName = table.Column<string>(maxLength: 100, nullable: false),
            //        IsDeleted = table.Column<bool>(nullable: true),
            //        LastModificationTime = table.Column<DateTime>(nullable: true),
            //        LastModifierUserId = table.Column<long>(nullable: true),
            //        ParentId = table.Column<int>(nullable: true),
            //        TenantId = table.Column<int>(nullable: true),
            //        OrganizationType = table.Column<int>(nullable: true),
            //        DataBaseConnection = table.Column<string>(nullable: true),
            //        ERPOrganizationLeader = table.Column<int>(nullable: true),
            //        ERPOrganization = table.Column<int>(nullable: true),
            //        Remark = table.Column<string>(nullable: true),
            //        FWorkshopType = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_t_Organizationunit", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_t_Organizationunit_t_Organizationunit_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "t_Organizationunit",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateTable(
                name: "t_SubMesType",
                columns: table => new
                {
                    FTypeID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FName = table.Column<string>(nullable: true),
                    FDetail = table.Column<int>(nullable: false),
                    FType = table.Column<int>(nullable: false),
                    FGRType = table.Column<int>(nullable: false),
                    FModifyTime = table.Column<byte[]>(nullable: true),
                    UUID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_SubMesType", x => x.FTypeID);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Employee_FMpno",
            //    table: "Employee",
            //    column: "FMpno",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_t_Organizationunit_ParentId",
            //    table: "t_Organizationunit",
            //    column: "ParentId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Employee_t_Organizationunit_FDepartment",
            //    table: "Employee",
            //    column: "FDepartment",
            //    principalTable: "t_Organizationunit",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_t_equipment_t_Organizationunit_FWorkCenterID",
            //    table: "t_equipment",
            //    column: "FWorkCenterID",
            //    principalTable: "t_Organizationunit",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_t_Organizationunit_FDepartment",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_t_equipment_t_Organizationunit_FWorkCenterID",
                table: "t_equipment");

            migrationBuilder.DropTable(
                name: "t_Organizationunit");

            migrationBuilder.DropTable(
                name: "t_SubMesType");

            migrationBuilder.DropIndex(
                name: "IX_Employee_FMpno",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ICMODispBillID",
                table: "ICMOInspectBill");

            migrationBuilder.AlterColumn<bool>(
                name: "FDeleted",
                table: "TB_BadItemRelation",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.CreateTable(
                name: "Organizationunit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 100, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DataBaseConnection = table.Column<string>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 100, nullable: false),
                    ERPOrganization = table.Column<int>(nullable: true),
                    ERPOrganizationLeader = table.Column<int>(nullable: true),
                    FWorkshopType = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    OrganizationType = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizationunit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizationunit_Organizationunit_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Organizationunit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organizationunit_ParentId",
                table: "Organizationunit",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Organizationunit_FDepartment",
                table: "Employee",
                column: "FDepartment",
                principalTable: "Organizationunit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_t_equipment_Organizationunit_FWorkCenterID",
                table: "t_equipment",
                column: "FWorkCenterID",
                principalTable: "Organizationunit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

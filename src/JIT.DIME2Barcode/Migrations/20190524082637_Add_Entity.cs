using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class Add_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_OrganizationUnit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 100, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    OrganizationType = table.Column<int>(nullable: true),
                    DataBaseConnection = table.Column<string>(nullable: true),
                    ERPOrganizationLeader = table.Column<int>(nullable: true),
                    ERPOrganization = table.Column<int>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OrganizationUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_OrganizationUnit_T_OrganizationUnit_ParentId",
                        column: x => x.ParentId,
                        principalTable: "T_OrganizationUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_OrganizationUnit_ParentId",
                table: "T_OrganizationUnit",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_OrganizationUnit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_equipment",
                table: "t_equipment");

            migrationBuilder.RenameTable(
                name: "t_equipment",
                newName: "Equipment");

            migrationBuilder.RenameIndex(
                name: "IX_t_equipment_FNumber",
                table: "Equipment",
                newName: "IX_Equipment_FNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipment",
                table: "Equipment",
                column: "FInterID");
        }
    }
}

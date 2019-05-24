using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class Add_Entity_Resource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "T_Equipment",
                columns: table => new
                {
                    FInterID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FNumber = table.Column<string>(nullable: true),
                    FName = table.Column<string>(nullable: true),
                    FType = table.Column<int>(nullable: false),
                    FWorkCenterID = table.Column<int>(nullable: true),
                    FStatus = table.Column<int>(nullable: false),
                    FDayWorkHours = table.Column<TimeSpan>(nullable: true),
                    FMaxWorkHours = table.Column<TimeSpan>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Equipment", x => x.FInterID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Equipment_FNumber",
                table: "T_Equipment",
                column: "FNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Equipment");

            
        }
    }
}

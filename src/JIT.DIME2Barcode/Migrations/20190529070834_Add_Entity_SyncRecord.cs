using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class Add_Entity_SyncRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "t_SyncRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TableName = table.Column<string>(nullable: true),
                    LastSyncTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_SyncRecord", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_SubMessage");

            migrationBuilder.DropTable(
                name: "t_SyncRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ICMOQualityRpt",
                table: "ICMOQualityRpt");

            migrationBuilder.DropColumn(
                name: "计划完工日期",
                table: "VW_MODispBillList");

            migrationBuilder.DropColumn(
                name: "计划开工日期",
                table: "VW_MODispBillList");

            migrationBuilder.DropColumn(
                name: "FItemNumber",
                table: "VW_ICMODaily_Group_By_Day");

            migrationBuilder.RenameTable(
                name: "ICMOQualityRpt",
                newName: "ICQualityRpt");

            migrationBuilder.AlterColumn<string>(
                name: "操作者",
                table: "VW_MODispBillList",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "FItemID",
                table: "VW_ICMODaily_Group_By_Day",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ICQualityRpt",
                table: "ICQualityRpt",
                column: "FID");
        }
    }
}

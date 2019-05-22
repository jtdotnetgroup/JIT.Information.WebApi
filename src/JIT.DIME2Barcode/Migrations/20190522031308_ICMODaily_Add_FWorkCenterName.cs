using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class ICMODaily_Add_FWorkCenterName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FWorkCenterName",
                table: "ICMODaily",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VW_ICMODaily",
                columns: table => new
                {
                    计划单号 = table.Column<string>(nullable: false),
                    日期 = table.Column<DateTime>(nullable: true),
                    任务单号 = table.Column<string>(nullable: true),
                    车间 = table.Column<int>(nullable: false),
                    产品编码 = table.Column<string>(nullable: true),
                    产品名称 = table.Column<string>(nullable: true),
                    规格型号 = table.Column<string>(nullable: true),
                    计划数量 = table.Column<decimal>(nullable: true),
                    计划开工日期 = table.Column<DateTime>(nullable: true),
                    计划完工日期 = table.Column<DateTime>(nullable: true),
                    完成数量 = table.Column<decimal>(nullable: true),
                    FID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VW_ICMODaily", x => x.计划单号);
                });

            migrationBuilder.CreateTable(
                name: "VW_MODispBillList",
                columns: table => new
                {
                    FID = table.Column<string>(nullable: false),
                    设备 = table.Column<string>(nullable: true),
                    工作中心 = table.Column<string>(nullable: true),
                    生产日期 = table.Column<DateTime>(nullable: false),
                    班次 = table.Column<int>(nullable: true),
                    生产任务 = table.Column<string>(nullable: true),
                    派工单号 = table.Column<string>(nullable: true),
                    操作者 = table.Column<string>(nullable: true),
                    产品代码 = table.Column<string>(nullable: true),
                    产品名称 = table.Column<string>(nullable: true),
                    规格型号 = table.Column<string>(nullable: true),
                    工序 = table.Column<string>(nullable: true),
                    计划数量 = table.Column<decimal>(nullable: true),
                    派工数量 = table.Column<decimal>(nullable: true),
                    汇报数量 = table.Column<decimal>(nullable: true),
                    合格数量 = table.Column<decimal>(nullable: true),
                    不合格数量 = table.Column<decimal>(nullable: true),
                    打印次数 = table.Column<int>(nullable: false),
                    FStatus = table.Column<int>(nullable: false),
                    FClosed = table.Column<bool>(nullable: true),
                    FItemID = table.Column<int>(nullable: true),
                    FWorkCenterID = table.Column<int>(nullable: true),
                    FsrcID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VW_MODispBillList", x => x.FID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VW_ICMODaily");

            migrationBuilder.DropTable(
                name: "VW_MODispBillList");

            migrationBuilder.DropColumn(
                name: "FWorkCenterName",
                table: "ICMODaily");
        }
    }
}

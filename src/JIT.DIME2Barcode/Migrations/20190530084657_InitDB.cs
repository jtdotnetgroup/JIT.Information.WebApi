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
                name: "ICBOM",
                columns: table => new
                {
                    FInterID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FBrNo = table.Column<string>(nullable: true),
                    FBOMNumber = table.Column<string>(nullable: true),
                    FImpMode = table.Column<short>(nullable: false),
                    FUseStatus = table.Column<int>(nullable: true),
                    FVersion = table.Column<string>(nullable: true),
                    FParentID = table.Column<int>(nullable: true),
                    FItemID = table.Column<int>(nullable: false),
                    FQty = table.Column<decimal>(nullable: false),
                    FYield = table.Column<decimal>(nullable: true),
                    FCheckID = table.Column<int>(nullable: true),
                    FCheckDate = table.Column<DateTime>(nullable: true),
                    FOperatorID = table.Column<int>(nullable: true),
                    FEnterTime = table.Column<DateTime>(nullable: true),
                    FStatus = table.Column<short>(nullable: false),
                    FCancellation = table.Column<bool>(nullable: true),
                    FTranType = table.Column<int>(nullable: false),
                    FRoutingID = table.Column<int>(nullable: false),
                    FBomType = table.Column<int>(nullable: false),
                    FCustID = table.Column<int>(nullable: false),
                    FCustItemID = table.Column<int>(nullable: false),
                    FAccessories = table.Column<int>(nullable: false),
                    FNote = table.Column<string>(nullable: true),
                    FUnitID = table.Column<int>(nullable: false),
                    FAUXQTY = table.Column<decimal>(nullable: false),
                    FCheckerID = table.Column<int>(nullable: true),
                    FAudDate = table.Column<DateTime>(nullable: true),
                    FEcnInterID = table.Column<int>(nullable: false),
                    FBeenChecked = table.Column<bool>(nullable: true),
                    FForbid = table.Column<short>(nullable: false),
                    FAuxPropID = table.Column<int>(nullable: false),
                    FPDMImportDate = table.Column<DateTime>(nullable: true),
                    FBOMSkip = table.Column<short>(nullable: false),
                    FClassTypeID = table.Column<int>(nullable: true),
                    FUserID = table.Column<int>(nullable: true),
                    FUseDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICBOM", x => x.FInterID);
                });

      
             
            migrationBuilder.CreateTable(
                name: "t_Department",
                columns: table => new
                {
                    FItemID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FBrNO = table.Column<string>(nullable: true),
                    FManager = table.Column<int>(nullable: true),
                    FPhone = table.Column<string>(nullable: true),
                    FFax = table.Column<string>(nullable: true),
                    FNote = table.Column<string>(nullable: true),
                    FNumber = table.Column<string>(nullable: true),
                    FName = table.Column<string>(nullable: true),
                    FParentID = table.Column<int>(nullable: true),
                    FDProperty = table.Column<int>(nullable: false),
                    FDStock = table.Column<int>(nullable: true),
                    FDeleted = table.Column<short>(nullable: false),
                    FShortNumber = table.Column<string>(nullable: true),
                    FAcctID = table.Column<int>(nullable: false),
                    FCostAccountType = table.Column<int>(nullable: false),
                    FModifyTime = table.Column<byte[]>(nullable: true),
                    FCalID = table.Column<int>(nullable: false),
                    FPlanArea = table.Column<int>(nullable: true),
                    FOtherARAcctID = table.Column<int>(nullable: false),
                    FOtherAPAcctID = table.Column<int>(nullable: false),
                    FPreARAcctID = table.Column<int>(nullable: false),
                    FPreAPAcctID = table.Column<int>(nullable: false),
                    FIsCreditMgr = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Department", x => x.FItemID);
                });
               

            migrationBuilder.CreateTable(
                name: "t_SubMessage",
                columns: table => new
                {
                    FInterID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FBrNo = table.Column<string>(nullable: true),
                    FID = table.Column<string>(nullable: true),
                    FParentID = table.Column<int>(nullable: false),
                    FName = table.Column<string>(nullable: true),
                    FTypeID = table.Column<int>(nullable: false),
                    FDetail = table.Column<int>(nullable: false),
                    FDeleted = table.Column<int>(nullable: false),
                    FModifyTime = table.Column<byte[]>(nullable: true),
                    FSystemType = table.Column<int>(nullable: false),
                    FSpec = table.Column<string>(nullable: true),
                    UUID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_SubMessage", x => x.FInterID);
                });
             
             
             
              

            migrationBuilder.CreateTable(
                name: "T_EquimentShift",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FEqiupmentID = table.Column<int>(nullable: false),
                    FEmployeeID = table.Column<int>(nullable: false),
                    FShift = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_EquimentShift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_EquimentShift_Employee_FEmployeeID",
                        column: x => x.FEmployeeID,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_EquimentShift_t_equipment_FEqiupmentID",
                        column: x => x.FEqiupmentID,
                        principalTable: "t_equipment",
                        principalColumn: "FInterID",
                        onDelete: ReferentialAction.Cascade);
                });
             
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillStatus");

            migrationBuilder.DropTable(
                name: "ICBOM");

            migrationBuilder.DropTable(
                name: "ICException");

            migrationBuilder.DropTable(
                name: "ICMaterialPicking");

            migrationBuilder.DropTable(
                name: "ICMO");

            migrationBuilder.DropTable(
                name: "ICMODaily");

            migrationBuilder.DropTable(
                name: "ICMODispBill");

            migrationBuilder.DropTable(
                name: "ICMOInspectBill");

            migrationBuilder.DropTable(
                name: "ICMOQualityRpt");

            migrationBuilder.DropTable(
                name: "SEOrder");

            migrationBuilder.DropTable(
                name: "t_Department");

            migrationBuilder.DropTable(
                name: "T_EquimentShift");

            migrationBuilder.DropTable(
                name: "t_MeasureUnit");

            migrationBuilder.DropTable(
                name: "T_PrintTemplate");

            migrationBuilder.DropTable(
                name: "t_SubMessage");

            migrationBuilder.DropTable(
                name: "TB_BadItemRelation");

            migrationBuilder.DropTable(
                name: "VW_DispatchBill_List");

            migrationBuilder.DropTable(
                name: "VW_Employee");

            migrationBuilder.DropTable(
                name: "VW_Group_ICMODaily");

            migrationBuilder.DropTable(
                name: "VW_ICMODaily");

            migrationBuilder.DropTable(
                name: "VW_ICMODaily_Group_By_Day");

            migrationBuilder.DropTable(
                name: "VW_MODispBillList");

            migrationBuilder.DropTable(
                name: "ICMOSchedule");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "t_equipment");

            migrationBuilder.DropTable(
                name: "T_OrganizationUnit");
        }
    }
}

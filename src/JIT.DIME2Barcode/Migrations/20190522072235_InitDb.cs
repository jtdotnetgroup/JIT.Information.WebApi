using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillStatus",
                columns: table => new
                {
                    FTranType = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FTranName = table.Column<string>(nullable: true),
                    FStatus = table.Column<int>(nullable: false),
                    FName = table.Column<string>(nullable: true),
                    FNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillStatus", x => x.FTranType);
                });

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
                    FOrganizationUnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

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
                name: "ICException",
                columns: table => new
                {
                    FID = table.Column<string>(nullable: false),
                    FSrcID = table.Column<string>(nullable: true),
                    FBiller = table.Column<string>(nullable: true),
                    FTime = table.Column<DateTime>(nullable: true),
                    FRemark = table.Column<string>(nullable: true),
                    FRecoverTime = table.Column<DateTime>(nullable: true),
                    FNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICException", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "ICMaterialPicking",
                columns: table => new
                {
                    FID = table.Column<string>(nullable: false),
                    FSrcID = table.Column<string>(nullable: true),
                    FEntryID = table.Column<int>(nullable: false),
                    FItemID = table.Column<int>(nullable: true),
                    FUnitID = table.Column<int>(nullable: true),
                    FBatchNo = table.Column<string>(nullable: true),
                    FAuxQty = table.Column<decimal>(nullable: true),
                    FBiller = table.Column<string>(nullable: true),
                    FDate = table.Column<DateTime>(nullable: true),
                    FNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICMaterialPicking", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "ICMO",
                columns: table => new
                {
                    FInterID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FBrNo = table.Column<string>(nullable: true),
                    FBillNo = table.Column<string>(nullable: true),
                    FTranType = table.Column<short>(nullable: false),
                    FStatus = table.Column<short>(nullable: false),
                    FMRP = table.Column<short>(nullable: false),
                    FType = table.Column<short>(nullable: true),
                    FWorkShop = table.Column<int>(nullable: true),
                    FItemID = table.Column<int>(nullable: false),
                    FQty = table.Column<decimal>(nullable: false),
                    FCommitQty = table.Column<decimal>(nullable: false),
                    FPlanCommitDate = table.Column<DateTime>(nullable: true),
                    FPlanFinishDate = table.Column<DateTime>(nullable: true),
                    FConveyerID = table.Column<int>(nullable: true),
                    FCommitDate = table.Column<DateTime>(nullable: true),
                    FCheckerID = table.Column<int>(nullable: true),
                    FCheckDate = table.Column<DateTime>(nullable: true),
                    FRequesterID = table.Column<int>(nullable: true),
                    FBillerID = table.Column<int>(nullable: true),
                    FSourceEntryID = table.Column<short>(nullable: false),
                    FClosed = table.Column<short>(nullable: false),
                    FNote = table.Column<string>(nullable: true),
                    FUnitID = table.Column<int>(nullable: false),
                    FAuxCommitQty = table.Column<decimal>(nullable: false),
                    FAuxQty = table.Column<decimal>(nullable: false),
                    FOrderInterID = table.Column<int>(nullable: true),
                    FPPOrderInterID = table.Column<int>(nullable: true),
                    FParentInterID = table.Column<int>(nullable: true),
                    FCancellation = table.Column<bool>(nullable: true),
                    FSupplyID = table.Column<int>(nullable: true),
                    FQtyFinish = table.Column<decimal>(nullable: false),
                    FQtyScrap = table.Column<decimal>(nullable: false),
                    FQtyForItem = table.Column<decimal>(nullable: true),
                    FQtyLost = table.Column<decimal>(nullable: false),
                    FPlanIssueDate = table.Column<DateTime>(nullable: true),
                    FRoutingID = table.Column<int>(nullable: true),
                    FStartDate = table.Column<DateTime>(nullable: true),
                    FFinishDate = table.Column<DateTime>(nullable: true),
                    FAuxQtyFinish = table.Column<decimal>(nullable: false),
                    FAuxQtyScrap = table.Column<decimal>(nullable: false),
                    FAuxQtyForItem = table.Column<decimal>(nullable: true),
                    FAuxQtyLost = table.Column<decimal>(nullable: false),
                    FMrpClosed = table.Column<int>(nullable: false),
                    FBomInterID = table.Column<int>(nullable: false),
                    FQtyPass = table.Column<decimal>(nullable: false),
                    FAuxQtyPass = table.Column<decimal>(nullable: false),
                    FQtyBack = table.Column<decimal>(nullable: false),
                    FAuxQtyBack = table.Column<decimal>(nullable: false),
                    FFinishTime = table.Column<decimal>(nullable: false),
                    FReadyTIme = table.Column<decimal>(nullable: false),
                    FPowerCutTime = table.Column<decimal>(nullable: false),
                    FFixTime = table.Column<decimal>(nullable: false),
                    FWaitItemTime = table.Column<decimal>(nullable: false),
                    FWaitToolTime = table.Column<decimal>(nullable: false),
                    FTaskID = table.Column<int>(nullable: false),
                    FWorkTypeID = table.Column<int>(nullable: false),
                    FCostObjID = table.Column<int>(nullable: false),
                    FStockQty = table.Column<decimal>(nullable: false),
                    FAuxStockQty = table.Column<decimal>(nullable: false),
                    FSuspend = table.Column<bool>(nullable: true),
                    FProjectNO = table.Column<int>(nullable: true),
                    FProductionLineID = table.Column<int>(nullable: true),
                    FReleasedQty = table.Column<decimal>(nullable: false),
                    FReleasedAuxQty = table.Column<decimal>(nullable: false),
                    FUnScheduledQty = table.Column<decimal>(nullable: false),
                    FUnScheduledAuxQty = table.Column<decimal>(nullable: false),
                    FSubEntryID = table.Column<int>(nullable: false),
                    FScheduleID = table.Column<int>(nullable: false),
                    FPlanOrderInterID = table.Column<int>(nullable: false),
                    FProcessPrice = table.Column<decimal>(nullable: false),
                    FProcessFee = table.Column<decimal>(nullable: false),
                    FGMPBatchNo = table.Column<string>(nullable: true),
                    FGMPCollectRate = table.Column<decimal>(nullable: false),
                    FGMPItemBalance = table.Column<decimal>(nullable: false),
                    FGMPBulkQty = table.Column<decimal>(nullable: false),
                    FCustID = table.Column<int>(nullable: false),
                    FMultiCheckLevel1 = table.Column<int>(nullable: true),
                    FMultiCheckLevel2 = table.Column<int>(nullable: true),
                    FMultiCheckLevel3 = table.Column<int>(nullable: true),
                    FMultiCheckLevel4 = table.Column<int>(nullable: true),
                    FMultiCheckLevel5 = table.Column<int>(nullable: true),
                    FMultiCheckLevel6 = table.Column<int>(nullable: true),
                    FMultiCheckDate1 = table.Column<DateTime>(nullable: true),
                    FMultiCheckDate2 = table.Column<DateTime>(nullable: true),
                    FMultiCheckDate3 = table.Column<DateTime>(nullable: true),
                    FMultiCheckDate4 = table.Column<DateTime>(nullable: true),
                    FMultiCheckDate5 = table.Column<DateTime>(nullable: true),
                    FMultiCheckDate6 = table.Column<DateTime>(nullable: true),
                    FCurCheckLevel = table.Column<int>(nullable: true),
                    FMRPLockFlag = table.Column<int>(nullable: false),
                    FHandworkClose = table.Column<int>(nullable: false),
                    FConfirmerID = table.Column<int>(nullable: true),
                    FConfirmDate = table.Column<DateTime>(nullable: true),
                    FInHighLimit = table.Column<decimal>(nullable: false),
                    FInHighLimitQty = table.Column<decimal>(nullable: false),
                    FAuxInHighLimitQty = table.Column<decimal>(nullable: false),
                    FInLowLimit = table.Column<decimal>(nullable: false),
                    FInLowLimitQty = table.Column<decimal>(nullable: false),
                    FAuxInLowLimitQty = table.Column<decimal>(nullable: false),
                    FChangeTimes = table.Column<int>(nullable: false),
                    FCheckCommitQty = table.Column<decimal>(nullable: false),
                    FAuxCheckCommitQty = table.Column<decimal>(nullable: false),
                    FCloseDate = table.Column<DateTime>(nullable: true),
                    FPlanConfirmed = table.Column<int>(nullable: false),
                    FPlanMode = table.Column<int>(nullable: false),
                    FMTONo = table.Column<string>(nullable: true),
                    FPrintCount = table.Column<int>(nullable: false),
                    FFinClosed = table.Column<int>(nullable: false),
                    FFinCloseer = table.Column<int>(nullable: true),
                    FFinClosedate = table.Column<DateTime>(nullable: true),
                    FStockFlag = table.Column<int>(nullable: false),
                    FStartFlag = table.Column<int>(nullable: false),
                    FVchBillNo = table.Column<string>(nullable: true),
                    FVchInterID = table.Column<int>(nullable: false),
                    FCardClosed = table.Column<int>(nullable: false),
                    FHRReadyTime = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICMO", x => x.FInterID);
                });

            migrationBuilder.CreateTable(
                name: "ICMODispBill",
                columns: table => new
                {
                    FID = table.Column<string>(nullable: false),
                    FSrcID = table.Column<string>(nullable: true),
                    FBillNo = table.Column<string>(nullable: true),
                    FTranType = table.Column<int>(nullable: false),
                    FStatus = table.Column<int>(nullable: false),
                    FClosed = table.Column<bool>(nullable: true),
                    FMOInterID = table.Column<int>(nullable: true),
                    FMOBillNo = table.Column<string>(nullable: true),
                    FDate = table.Column<DateTime>(nullable: true),
                    FShift = table.Column<int>(nullable: true),
                    FOperID = table.Column<int>(nullable: true),
                    FWorkCenterID = table.Column<int>(nullable: true),
                    FMachineID = table.Column<int>(nullable: true),
                    FWorker = table.Column<string>(nullable: true),
                    FPlanAuxQty = table.Column<decimal>(nullable: true),
                    FCommitAuxQty = table.Column<decimal>(nullable: true),
                    FFinishAuxQty = table.Column<decimal>(nullable: true),
                    FFInspectAuxQty = table.Column<decimal>(nullable: true),
                    FPassAuxQty = table.Column<decimal>(nullable: true),
                    FFailAuxQty = table.Column<decimal>(nullable: true),
                    FBiller = table.Column<string>(nullable: true),
                    FBillTime = table.Column<DateTime>(nullable: true),
                    FChecker = table.Column<string>(nullable: true),
                    FCheckTime = table.Column<DateTime>(nullable: true),
                    FCloser = table.Column<string>(nullable: true),
                    FCloseTime = table.Column<DateTime>(nullable: true),
                    FPrintCount = table.Column<int>(nullable: false),
                    FNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICMODispBill", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "ICMOInspectBill",
                columns: table => new
                {
                    FID = table.Column<string>(nullable: false),
                    FMOInterID = table.Column<int>(nullable: true),
                    FBillNo = table.Column<string>(nullable: true),
                    FTranType = table.Column<int>(nullable: false),
                    FStatus = table.Column<int>(nullable: false),
                    FOperID = table.Column<int>(nullable: true),
                    FWorkcenterID = table.Column<int>(nullable: true),
                    FMachineID = table.Column<int>(nullable: true),
                    FAuxQty = table.Column<decimal>(nullable: true),
                    FCheckAuxQty = table.Column<decimal>(nullable: true),
                    FPassAuxQty = table.Column<decimal>(nullable: true),
                    FFailAuxQty = table.Column<decimal>(nullable: true),
                    FFailAuxQtyP = table.Column<decimal>(nullable: true),
                    FFailAuxQtyM = table.Column<decimal>(nullable: true),
                    FPassAuxQtyP = table.Column<decimal>(nullable: true),
                    FPassAuxQtyM = table.Column<decimal>(nullable: true),
                    FNote = table.Column<string>(nullable: true),
                    FWorker = table.Column<string>(nullable: true),
                    FInspector = table.Column<string>(nullable: true),
                    FInspectTime = table.Column<DateTime>(nullable: true),
                    FBiller = table.Column<string>(nullable: true),
                    FBillTime = table.Column<DateTime>(nullable: true),
                    FChecker = table.Column<string>(nullable: true),
                    FCheckTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICMOInspectBill", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "ICMOSchedule",
                columns: table => new
                {
                    FID = table.Column<string>(nullable: false),
                    FBillNo = table.Column<string>(nullable: true),
                    FTranType = table.Column<int>(nullable: false),
                    FMOInterID = table.Column<int>(nullable: false),
                    FMOBillNo = table.Column<string>(nullable: true),
                    FStatus = table.Column<int>(nullable: false),
                    FCancellation = table.Column<bool>(nullable: true),
                    FClosed = table.Column<bool>(nullable: true),
                    FSrcAuxQty = table.Column<decimal>(nullable: true),
                    FPlanAuxQty = table.Column<decimal>(nullable: true),
                    FFinishAuxQty = table.Column<decimal>(nullable: true),
                    FPassAuxQty = table.Column<decimal>(nullable: true),
                    FFailAuxQty = table.Column<decimal>(nullable: true),
                    FPlanBeginDate = table.Column<DateTime>(nullable: true),
                    FPlanFinishDate = table.Column<DateTime>(nullable: true),
                    FFinishDate = table.Column<DateTime>(nullable: true),
                    FBiller = table.Column<string>(nullable: true),
                    FBillTime = table.Column<DateTime>(nullable: true),
                    FChecker = table.Column<string>(nullable: true),
                    FCheckTime = table.Column<DateTime>(nullable: true),
                    FCloser = table.Column<string>(nullable: true),
                    FCloseTime = table.Column<DateTime>(nullable: true),
                    FNote = table.Column<string>(nullable: true),
                    FItemID = table.Column<string>(nullable: true),
                    FItemName = table.Column<string>(nullable: true),
                    FItemModel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICMOSchedule", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "ICQualityRpt",
                columns: table => new
                {
                    FID = table.Column<string>(nullable: false),
                    FItemID = table.Column<int>(nullable: false),
                    FAuxQty = table.Column<decimal>(nullable: true),
                    FRemark = table.Column<string>(nullable: true),
                    FNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICQualityRpt", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationUnitsJts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 100, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<int>(nullable: true),
                    DeleterUserId = table.Column<int>(nullable: true),
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
                    table.PrimaryKey("PK_OrganizationUnitsJts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SEOrder",
                columns: table => new
                {
                    FInterID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FBrNo = table.Column<string>(nullable: true),
                    FBillNo = table.Column<string>(nullable: true),
                    FCurrencyID = table.Column<int>(nullable: true),
                    FCustID = table.Column<int>(nullable: true),
                    FDate = table.Column<DateTime>(nullable: true),
                    FPayStyle = table.Column<string>(nullable: true),
                    FPayDate = table.Column<DateTime>(nullable: true),
                    FFetchStyle = table.Column<string>(nullable: true),
                    FFetchDate = table.Column<DateTime>(nullable: true),
                    FFetchAdd = table.Column<string>(nullable: true),
                    FSaleStyle = table.Column<int>(nullable: true),
                    FDeptID = table.Column<int>(nullable: true),
                    FEmpID = table.Column<int>(nullable: true),
                    FCheckerID = table.Column<int>(nullable: true),
                    FBillerID = table.Column<int>(nullable: true),
                    FNote = table.Column<string>(nullable: true),
                    FClosed = table.Column<short>(nullable: false),
                    FTranType = table.Column<int>(nullable: false),
                    FInvoiceClosed = table.Column<short>(nullable: true),
                    FBClosed = table.Column<short>(nullable: false),
                    FMangerID = table.Column<int>(nullable: true),
                    FSettleID = table.Column<int>(nullable: true),
                    FExchangeRate = table.Column<double>(nullable: true),
                    FDiscountType = table.Column<bool>(nullable: true),
                    FStatus = table.Column<short>(nullable: false),
                    FCancellation = table.Column<bool>(nullable: true),
                    FMultiCheckLevel1 = table.Column<int>(nullable: true),
                    FMultiCheckLevel2 = table.Column<int>(nullable: true),
                    FMultiCheckLevel3 = table.Column<int>(nullable: true),
                    FMultiCheckLevel4 = table.Column<int>(nullable: true),
                    FMultiCheckLevel5 = table.Column<int>(nullable: true),
                    FMultiCheckLevel6 = table.Column<int>(nullable: true),
                    FMultiCheckDate1 = table.Column<DateTime>(nullable: true),
                    FMultiCheckDate2 = table.Column<DateTime>(nullable: true),
                    FMultiCheckDate3 = table.Column<DateTime>(nullable: true),
                    FMultiCheckDate4 = table.Column<DateTime>(nullable: true),
                    FMultiCheckDate5 = table.Column<DateTime>(nullable: true),
                    FMultiCheckDate6 = table.Column<DateTime>(nullable: true),
                    FCurCheckLevel = table.Column<int>(nullable: true),
                    FTransitAheadTime = table.Column<float>(nullable: true),
                    FPOOrdBillNo = table.Column<string>(nullable: true),
                    FRelateBrID = table.Column<int>(nullable: true),
                    FImport = table.Column<int>(nullable: false),
                    FOrderAffirm = table.Column<int>(nullable: true),
                    FTranStatus = table.Column<int>(nullable: true),
                    FUUID = table.Column<Guid>(nullable: false),
                    FOperDate = table.Column<byte[]>(nullable: true),
                    FSystemType = table.Column<int>(nullable: false),
                    FCashDiscount = table.Column<string>(nullable: true),
                    FCheckDate = table.Column<DateTime>(nullable: true),
                    FExplanation = table.Column<string>(nullable: true),
                    FSettleDate = table.Column<DateTime>(nullable: true),
                    FSelTranType = table.Column<int>(nullable: false),
                    FChildren = table.Column<int>(nullable: false),
                    FBrID = table.Column<int>(nullable: true),
                    FAreaPS = table.Column<int>(nullable: false),
                    FClassTypeID = table.Column<int>(nullable: false),
                    FManageType = table.Column<int>(nullable: true),
                    FSysStatus = table.Column<short>(nullable: false),
                    FVersionNo = table.Column<string>(nullable: true),
                    FChangeDate = table.Column<DateTime>(nullable: true),
                    FChangeCauses = table.Column<string>(nullable: true),
                    FChangeMark = table.Column<int>(nullable: false),
                    FChangeUser = table.Column<int>(nullable: false),
                    FValidaterName = table.Column<string>(nullable: true),
                    FConsignee = table.Column<string>(nullable: true),
                    FDrpRelateTranType = table.Column<int>(nullable: false),
                    FPrintCount = table.Column<short>(nullable: false),
                    FExchangeRateType = table.Column<int>(nullable: false),
                    FHeadSelfS0150 = table.Column<int>(nullable: true),
                    FHeadSelfS0151 = table.Column<int>(nullable: true),
                    FHeadSelfS0153 = table.Column<DateTime>(nullable: true),
                    FHeadSelfS0154 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEOrder", x => x.FInterID);
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
                name: "t_MeasureUnit",
                columns: table => new
                {
                    FMeasureUnitID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FUnitGroupID = table.Column<int>(nullable: false),
                    FNumber = table.Column<string>(nullable: true),
                    FAuxClass = table.Column<string>(nullable: true),
                    FName = table.Column<string>(nullable: true),
                    FConversation = table.Column<int>(nullable: true),
                    FCoefficient = table.Column<decimal>(nullable: false),
                    FPrecision = table.Column<int>(nullable: false),
                    FBrNo = table.Column<string>(nullable: true),
                    FItemID = table.Column<int>(nullable: false),
                    FParentID = table.Column<int>(nullable: true),
                    FDeleted = table.Column<short>(nullable: false),
                    FShortNumber = table.Column<string>(nullable: true),
                    FOperDate = table.Column<string>(nullable: true),
                    FScale = table.Column<decimal>(nullable: false),
                    FStandard = table.Column<short>(nullable: false),
                    FControl = table.Column<short>(nullable: false),
                    FModifyTime = table.Column<byte[]>(nullable: true),
                    FSystemType = table.Column<int>(nullable: false),
                    UUID = table.Column<Guid>(nullable: false),
                    FNameEN = table.Column<string>(nullable: true),
                    FNameEnPlu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_MeasureUnit", x => x.FMeasureUnitID);
                });

            migrationBuilder.CreateTable(
                name: "T_PrintTemplate",
                columns: table => new
                {
                    FInterID = table.Column<string>(nullable: false),
                    FUserID = table.Column<int>(nullable: true),
                    FTranType = table.Column<int>(nullable: false),
                    FType = table.Column<string>(nullable: true),
                    FItemID = table.Column<int>(nullable: false),
                    FFileDir = table.Column<string>(nullable: true),
                    FFile = table.Column<byte[]>(nullable: true),
                    FNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PrintTemplate", x => x.FInterID);
                });

            migrationBuilder.CreateTable(
                name: "TB_BadItemRelation",
                columns: table => new
                {
                    FID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FItemID = table.Column<int>(nullable: true),
                    FOperID = table.Column<int>(nullable: false),
                    FNumber = table.Column<string>(nullable: true),
                    FName = table.Column<string>(nullable: true),
                    FDeleted = table.Column<bool>(nullable: true),
                    FRemark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_BadItemRelation", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "ICMODaily",
                columns: table => new
                {
                    FID = table.Column<string>(nullable: false),
                    FSrcID = table.Column<string>(nullable: true),
                    FBillNo = table.Column<string>(nullable: true),
                    FTranType = table.Column<int>(nullable: true),
                    FStatus = table.Column<int>(nullable: false),
                    FClosed = table.Column<bool>(nullable: true),
                    FMOInterID = table.Column<int>(nullable: true),
                    FMOBillNo = table.Column<string>(nullable: true),
                    FEntryID = table.Column<int>(nullable: false),
                    FDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FShift = table.Column<int>(nullable: true),
                    FWorkCenterID = table.Column<int>(nullable: true),
                    FMachineID = table.Column<int>(nullable: true),
                    FWorker = table.Column<string>(nullable: true),
                    FPlanAuxQty = table.Column<decimal>(nullable: true),
                    FCommitAuxQty = table.Column<decimal>(nullable: true),
                    FFinishAuxQty = table.Column<decimal>(nullable: true),
                    FPassAuxQty = table.Column<decimal>(nullable: true),
                    FFailAuxQty = table.Column<decimal>(nullable: true),
                    FOperID = table.Column<int>(nullable: true),
                    FOperNote = table.Column<string>(nullable: true),
                    FBiller = table.Column<string>(nullable: true),
                    FBillTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    FChecker = table.Column<string>(nullable: true),
                    FCheckTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    FCloser = table.Column<string>(nullable: true),
                    FCloseTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    FNote = table.Column<string>(nullable: true),
                    FWorkCenterName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICMODaily", x => x.FID);
                    table.ForeignKey(
                        name: "FK_ICMODaily_ICMOSchedule_FSrcID",
                        column: x => x.FSrcID,
                        principalTable: "ICMOSchedule",
                        principalColumn: "FID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ICMODaily_FSrcID",
                table: "ICMODaily",
                column: "FSrcID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillStatus");

            migrationBuilder.DropTable(
                name: "Employee");

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
                name: "ICQualityRpt");

            migrationBuilder.DropTable(
                name: "OrganizationUnitsJts");

            migrationBuilder.DropTable(
                name: "SEOrder");

            migrationBuilder.DropTable(
                name: "t_Department");

            migrationBuilder.DropTable(
                name: "t_MeasureUnit");

            migrationBuilder.DropTable(
                name: "T_PrintTemplate");

            migrationBuilder.DropTable(
                name: "TB_BadItemRelation");

            migrationBuilder.DropTable(
                name: "ICMOSchedule");
        }
    }
}

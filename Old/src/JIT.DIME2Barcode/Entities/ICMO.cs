using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    public partial class ICMO:Entity<int>
    {
        [NotMapped]
        public override int Id { get; set; }
        public string FBrNo { get; set; }
        [Key]
        public int FInterID { get; set; }
        public string FBillNo { get; set; }
        public short FTranType { get; set; }
        public short FStatus { get; set; }
        public short FMRP { get; set; }
        public short? FType { get; set; }
        public int? FWorkShop { get; set; }
        public int FItemID { get; set; }
        public decimal FQty { get; set; }
        public decimal FCommitQty { get; set; }
        public DateTime? FPlanCommitDate { get; set; }
        public DateTime? FPlanFinishDate { get; set; }
        public int? FConveyerID { get; set; }
        public DateTime? FCommitDate { get; set; }
        public int? FCheckerID { get; set; }
        public DateTime? FCheckDate { get; set; }
        public int? FRequesterID { get; set; }
        public int? FBillerID { get; set; }
        public short FSourceEntryID { get; set; }
        public short FClosed { get; set; }
        public string FNote { get; set; }
        public int FUnitID { get; set; }
        public decimal FAuxCommitQty { get; set; }
        public decimal FAuxQty { get; set; }
        public int? FOrderInterID { get; set; }
        public int? FPPOrderInterID { get; set; }
        public int? FParentInterID { get; set; }
        public bool? FCancellation { get; set; }
        public int? FSupplyID { get; set; }
        public decimal FQtyFinish { get; set; }
        public decimal FQtyScrap { get; set; }
        public decimal? FQtyForItem { get; set; }
        public decimal FQtyLost { get; set; }
        public DateTime? FPlanIssueDate { get; set; }
        public int? FRoutingID { get; set; }
        public DateTime? FStartDate { get; set; }
        public DateTime? FFinishDate { get; set; }
        public decimal FAuxQtyFinish { get; set; }
        public decimal FAuxQtyScrap { get; set; }
        public decimal? FAuxQtyForItem { get; set; }
        public decimal FAuxQtyLost { get; set; }
        public int FMrpClosed { get; set; }
        public int FBomInterID { get; set; }
        public decimal FQtyPass { get; set; }
        public decimal FAuxQtyPass { get; set; }
        public decimal FQtyBack { get; set; }
        public decimal FAuxQtyBack { get; set; }
        public decimal FFinishTime { get; set; }
        public decimal FReadyTIme { get; set; }
        public decimal FPowerCutTime { get; set; }
        public decimal FFixTime { get; set; }
        public decimal FWaitItemTime { get; set; }
        public decimal FWaitToolTime { get; set; }
        public int FTaskID { get; set; }
        public int FWorkTypeID { get; set; }
        public int FCostObjID { get; set; }
        public decimal FStockQty { get; set; }
        public decimal FAuxStockQty { get; set; }
        public bool? FSuspend { get; set; }
        public int? FProjectNO { get; set; }
        public int? FProductionLineID { get; set; }
        public decimal FReleasedQty { get; set; }
        public decimal FReleasedAuxQty { get; set; }
        public decimal FUnScheduledQty { get; set; }
        public decimal FUnScheduledAuxQty { get; set; }
        public int FSubEntryID { get; set; }
        public int FScheduleID { get; set; }
        public int FPlanOrderInterID { get; set; }
        public decimal FProcessPrice { get; set; }
        public decimal FProcessFee { get; set; }
        public string FGMPBatchNo { get; set; }
        public decimal FGMPCollectRate { get; set; }
        public decimal FGMPItemBalance { get; set; }
        public decimal FGMPBulkQty { get; set; }
        public int FCustID { get; set; }
        public int? FMultiCheckLevel1 { get; set; }
        public int? FMultiCheckLevel2 { get; set; }
        public int? FMultiCheckLevel3 { get; set; }
        public int? FMultiCheckLevel4 { get; set; }
        public int? FMultiCheckLevel5 { get; set; }
        public int? FMultiCheckLevel6 { get; set; }
        public DateTime? FMultiCheckDate1 { get; set; }
        public DateTime? FMultiCheckDate2 { get; set; }
        public DateTime? FMultiCheckDate3 { get; set; }
        public DateTime? FMultiCheckDate4 { get; set; }
        public DateTime? FMultiCheckDate5 { get; set; }
        public DateTime? FMultiCheckDate6 { get; set; }
        public int? FCurCheckLevel { get; set; }
        public int FMRPLockFlag { get; set; }
        public int FHandworkClose { get; set; }
        public int? FConfirmerID { get; set; }
        public DateTime? FConfirmDate { get; set; }
        public decimal FInHighLimit { get; set; }
        public decimal FInHighLimitQty { get; set; }
        public decimal FAuxInHighLimitQty { get; set; }
        public decimal FInLowLimit { get; set; }
        public decimal FInLowLimitQty { get; set; }
        public decimal FAuxInLowLimitQty { get; set; }
        public int FChangeTimes { get; set; }
        public decimal FCheckCommitQty { get; set; }
        public decimal FAuxCheckCommitQty { get; set; }
        public DateTime? FCloseDate { get; set; }
        public int FPlanConfirmed { get; set; }
        public int FPlanMode { get; set; }
        public string FMTONo { get; set; }
        public int FPrintCount { get; set; }
        public int FFinClosed { get; set; }
        public int? FFinCloseer { get; set; }
        public DateTime? FFinClosedate { get; set; }
        public int FStockFlag { get; set; }
        public int FStartFlag { get; set; }
        public string FVchBillNo { get; set; }
        public int FVchInterID { get; set; }
        public int FCardClosed { get; set; }
        public decimal FHRReadyTime { get; set; }
    }
}
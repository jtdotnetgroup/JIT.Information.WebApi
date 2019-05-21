using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JIT.DIME2Barcode.Entities
{
    public partial class SEOrder
    {
        public string FBrNo { get; set; }
        [Key]
        public int FInterID { get; set; }
        public string FBillNo { get; set; }
        public int? FCurrencyID { get; set; }
        public int? FCustID { get; set; }
        public DateTime? FDate { get; set; }
        public string FPayStyle { get; set; }
        public DateTime? FPayDate { get; set; }
        public string FFetchStyle { get; set; }
        public DateTime? FFetchDate { get; set; }
        public string FFetchAdd { get; set; }
        public int? FSaleStyle { get; set; }
        public int? FDeptID { get; set; }
        public int? FEmpID { get; set; }
        public int? FCheckerID { get; set; }
        public int? FBillerID { get; set; }
        public string FNote { get; set; }
        public short FClosed { get; set; }
        public int FTranType { get; set; }
        public short? FInvoiceClosed { get; set; }
        public short FBClosed { get; set; }
        public int? FMangerID { get; set; }
        public int? FSettleID { get; set; }
        public double? FExchangeRate { get; set; }
        public bool? FDiscountType { get; set; }
        public short FStatus { get; set; }
        public bool? FCancellation { get; set; }
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
        public float? FTransitAheadTime { get; set; }
        public string FPOOrdBillNo { get; set; }
        public int? FRelateBrID { get; set; }
        public int FImport { get; set; }
        public int? FOrderAffirm { get; set; }
        public int? FTranStatus { get; set; }
        public Guid FUUID { get; set; }
        public byte[] FOperDate { get; set; }
        public int FSystemType { get; set; }
        public string FCashDiscount { get; set; }
        public DateTime? FCheckDate { get; set; }
        public string FExplanation { get; set; }
        public DateTime? FSettleDate { get; set; }
        public int FSelTranType { get; set; }
        public int FChildren { get; set; }
        public int? FBrID { get; set; }
        public int FAreaPS { get; set; }
        public int FClassTypeID { get; set; }
        public int? FManageType { get; set; }
        public short FSysStatus { get; set; }
        public string FVersionNo { get; set; }
        public DateTime? FChangeDate { get; set; }
        public string FChangeCauses { get; set; }
        public int FChangeMark { get; set; }
        public int FChangeUser { get; set; }
        public string FValidaterName { get; set; }
        public string FConsignee { get; set; }
        public int FDrpRelateTranType { get; set; }
        public short FPrintCount { get; set; }
        public int FExchangeRateType { get; set; }
        public int? FHeadSelfS0150 { get; set; }
        public int? FHeadSelfS0151 { get; set; }
        public DateTime? FHeadSelfS0153 { get; set; }
        public int? FHeadSelfS0154 { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODispBill.Dtos
{
    public class ICMODispBillListDto : EntityDto<string>
    {
        public string FSrcID { get; set; }
        public System.DateTime 日期 { get; set; }
        public int? 机台 { get; set; }
        public int? 班组 { get; set; }
        public string 操作员 { get; set; }
        public decimal 派工数量 { get; set; }
        public decimal 完成数量 { get; set; }
        public decimal 合格数量 { get; set; }
        public string FMOBillNo { get; set; }
        public int? FMOInterID { get; set; }
        public decimal 计划数量 { get; set; }
    }
}
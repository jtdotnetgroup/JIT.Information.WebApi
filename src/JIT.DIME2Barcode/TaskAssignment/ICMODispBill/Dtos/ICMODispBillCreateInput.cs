using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODispBill.Dtos
{
    public class ICMODispBillCreateInput:EntityDto<string>
    {
        

        [Required]
        public DispBillCreateInput[] Details { get; set; }
        
    }

    [AutoMapTo(typeof(Entities.ICMODispBill))]
    public class DispBillCreateInput
    {
        //源单ID，即ICMODaily的FID
        public string FSrcID { get; set; }
        //班次
        public int FShift { get; set; }
        //设备
        public int FMachineID { get; set; }
        //车间
        public int FWorkCenterID { get; set; }
        //派工数量
        public decimal? FCommitAuxQty { get; set; }
        //操作员
        public string FWorker { get; set; }
        [Required]
        public string FMOBillNo { get; set; }
        [Required]
        public int? FMOInterID { get; set; }
    }
}
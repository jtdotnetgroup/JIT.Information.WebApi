using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODispBill.Dtos
{
    public class ICMODispBillGetAllInput:PagedResultRequestDto
    {
        public  DateTime? FDate { get; set; }

        [Required]
        public string FMOBillNo { get; set; }
        
        [Required]
        public int FMOInterID { get; set; }
    }
}
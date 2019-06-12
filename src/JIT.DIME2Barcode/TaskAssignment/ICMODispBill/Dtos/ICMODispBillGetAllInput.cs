using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODispBill.Dtos
{
    public class ICMODispBillGetAllInput:PagedResultRequestDto
    {
        public  DateTime? FDate { get; set; }

        [Required]
        public string FMOBillNo { get; set; }
        
        public int FMOInterID { get; set; }

      
    }

    public class ICMODispBillGetAllInputTwo : PagedResultRequestDto
    {
        public string FSrcID { get; set; }
    }

    public class ICMODispBill_Daily_GetAllListInput
    {
        [Required]
        public DateTime[] DatelList { get; set; }
        [Required]
        public string[] FMOBillNos { get; set; }
    }
}
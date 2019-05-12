using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODispBill.Dtos
{
    public class ICMODispBillGetAllInput:PagedResultRequestDto
    {
        [Required]
        public  DateTime FDate { get; set; }
        
    }
}
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos
{
    public class ICMODailyInput:EntityDto<string>
    {
        [Required]
        public string FID { get; set; }
    }
}
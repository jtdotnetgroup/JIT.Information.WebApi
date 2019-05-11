using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos
{
    public class ICMODailyCreatDto:EntityDto<string>
    {
        [Required]
        public int FMOInterID { get; set; }
        [Required]
        public string FMOBillNo { get; set; }
        public DailyDto[] Dailies { get; set; }
    }
}
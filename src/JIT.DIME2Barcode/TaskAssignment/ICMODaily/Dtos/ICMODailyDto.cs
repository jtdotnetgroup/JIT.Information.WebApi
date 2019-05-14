using System;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos
{
    public class ICMODailyDto:EntityDto<string>
    {
       public string FSrcID { get; set; }
       public DateTime FDate { get; set; }

    }
}
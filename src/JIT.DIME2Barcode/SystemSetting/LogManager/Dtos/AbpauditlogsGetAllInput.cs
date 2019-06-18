using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.SystemSetting.LogManager.Dtos
{
   public class AbpauditlogsGetAllInput : PagedResultRequestDto
    {

        public  DateTime? StartTime { get; set; }
        public  DateTime? EndTime { get; set; }
        public  string Message { get; set; }
        public  bool Exception { get; set; }

    }
}

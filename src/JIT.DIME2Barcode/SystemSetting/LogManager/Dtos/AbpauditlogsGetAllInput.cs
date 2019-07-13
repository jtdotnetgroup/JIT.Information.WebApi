using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using JIT.DIME2Barcode.Model;
using JIT.InformationSystem.CommonClass;

namespace JIT.DIME2Barcode.SystemSetting.LogManager.Dtos
{
   public class AbpauditlogsGetAllInput : JITPagedResultRequestDto
    {

        public  DateTime? StartTime { get; set; }
        public  DateTime? EndTime { get; set; }
        public  string Message { get; set; }
        public  bool Exception { get; set; }

    }
}

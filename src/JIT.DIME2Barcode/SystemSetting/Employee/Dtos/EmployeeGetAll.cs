using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using JIT.DIME2Barcode.Model;
using JIT.InformationSystem.CommonClass;

namespace JIT.DIME2Barcode.SystemSetting.Employee.Dtos
{
   public class EmployeeGetAll: JITPagedResultRequestDto
    {
        [Required]
        public int Id { get; set; }

    }
}

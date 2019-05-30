using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.SystemSetting.Employee.Dtos
{
   public class EmployeeGetAll: PagedResultRequestDto
    {
        [Required]
        public int Id { get; set; }

    }
}

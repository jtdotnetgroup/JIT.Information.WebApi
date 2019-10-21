using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.t_equipment.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class t_equipmentListInputDto: PagedAndSortedResultRequestDto
    {
        public string FName { get; set; }
        public int FWorkCenterID { get; set; }
    }
}

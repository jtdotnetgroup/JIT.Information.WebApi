using System;
using System.Collections.Generic;
using System.Text;

namespace JIT.DIME2Barcode.TaskAssignment.t_equipment.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class t_equipmentListDto
    {
        public int FInterID { get; set; }
        public string FNumber { get; set; }
        public string FName { get; set; }
        public string DisplayName { get; set; }
        public int? FWorkCenterID { get; set; }
    }
}

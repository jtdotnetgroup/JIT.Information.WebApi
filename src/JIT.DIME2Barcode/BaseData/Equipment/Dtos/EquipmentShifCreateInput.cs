using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.BaseData.Equipment.Dtos
{
    public class EquipmentShiftCreateInput
    {
        public int EquipmentID { get; set; }
        public int EmployeeID { get; set; }
        public string ShiftName { get; set; }
    }

    public class EquipmentShiftDto : EntityDto
    {
        public int FEqiupmentID { get; set; }
        public int FEmployeeID { get; set; }
        public string FShift { get; set; }
    }
}
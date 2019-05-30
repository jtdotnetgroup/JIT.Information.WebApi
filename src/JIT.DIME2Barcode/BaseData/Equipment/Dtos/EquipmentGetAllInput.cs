using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.BaseData.Equipment.Dtos
{
    public class EquipmentGetAllInput:PagedAndSortedResultRequestDto
    {
        public int? OrganizationID { get; set; }
    }
}
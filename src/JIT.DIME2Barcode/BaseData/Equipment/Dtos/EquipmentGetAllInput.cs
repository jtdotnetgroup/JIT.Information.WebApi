using System.ComponentModel;
using Abp.Application.Services.Dto;
using JIT.InformationSystem.CommonClass;

namespace JIT.DIME2Barcode.BaseData.Equipment.Dtos
{
    public class EquipmentGetAllInput:JITPagedResultRequestDto
    {
        public int? OrganizationID { get; set; }

        public string OrganizationCode { get; set; }

        [DisplayName("资源编号")]
        public string FNumber { get; set; } 
        [DisplayName("名称")]
        public string FName { get; set; }
        [DisplayName("日工作时长")]
        public int FDayWorkHours { get; set; } 
        [DisplayName("最大工作时长")]
        public int FMaxWorkHours { get; set; } 
        [DisplayName("使用寿命")]
        public int FLift { get; set; } 
        [DisplayName("剩余寿命")]
        public int FResidualLife { get; set; }
    }
}
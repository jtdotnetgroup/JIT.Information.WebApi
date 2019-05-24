using System;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.BaseData.Equipment.Dtos
{
    public class EquipmentDto
    {
        public int FInterID { get; set; }
        //资源编号
        public string FNumber { get; set; }
        //名称
        public string FName { get; set; }
        //类型
        public int FType { get; set; }
        //工作中心ID
        public Nullable<int> FWorkCenterID { get; set; }
        //状态
        public int FStatus { get; set; }
        //日工作时长
        public Nullable<TimeSpan> FDayWorkHours { get; set; }
        //最大工作时长
        public Nullable<TimeSpan> FMaxWorkHours { get; set; }
        //备注
        public string Note { get; set; }
    }
}
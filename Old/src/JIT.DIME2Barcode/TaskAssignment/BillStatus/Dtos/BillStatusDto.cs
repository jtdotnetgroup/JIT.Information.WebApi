using Abp.Application.Services.Dto;

namespace JIT.JIT.TaskAssignment.BillStatus.Dtos
{
    public class BillStatusDto:EntityDto<int>
    {
        public int FTranType { get; set; }
        public string FTranName { get; set; }
        public int FStatus { get; set; }
        public string FName { get; set; }
        public string FNote { get; set; }
    }
}
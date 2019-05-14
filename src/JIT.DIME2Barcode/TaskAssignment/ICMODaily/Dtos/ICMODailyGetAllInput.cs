using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos
{
    public class ICMODailyGetAllInput:PagedAndSortedResultRequestDto
    {
        public int FMOInterID { get; set; }
    }
}
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JIT.DIME2Barcode.TaskAssignment.ICMODispBill.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace JIT.DIME2Barcode.AppService
{
    public interface IICMODispBillAppService:IApplicationService
    {
        [HttpPost]
        Task<PagedResultDto<VW_DispatchBill_List>> GetDailyDispatchList(ICMODispBill_Daily_GetAllListInput input);

    }
}